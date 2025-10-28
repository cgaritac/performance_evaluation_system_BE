using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PACE.Interfaces.IRepositories;
using PACE.Interfaces.IServices;
using PACE.Models.ActivityModels;
using PACE.Models.CommonModels;
using PACE.Utils.Constants;
using PACE.Utils.Enums;
using PACE.Utils.Mappers;

namespace PACE.Services;

public class ActivityService(IActivityRepository activityRepository, IGoalService goalService, ILogger<ActivityService> logger, IUserService userService) : IActivityService
{
    private readonly IActivityRepository _activityRepository = activityRepository;
    private readonly IGoalService _goalService = goalService;
    private readonly ILogger<ActivityService> _logger = logger;
    private readonly IUserService _userService = userService;

    public async Task<ActionResultDTO<List<ActivityResponseDTO>>> GetActivitiesAsync(int goalId)
    {
        var goalExists = await _goalService.VerifyExistsGoalAsync(goalId);

        if (!goalExists)
            return ActionResultDTO<List<ActivityResponseDTO>>.Fail(ErrorConstants.NotFoundGoalError, ErrorType.NotFound);

        var activities = await _activityRepository.GetActivitiesByGoalId(goalId).ToListAsync();
        var activityDTOs = activities.Select(a => a.ToDto()).ToList();

        return ActionResultDTO<List<ActivityResponseDTO>>.Ok(activityDTOs);
    }

    public async Task<ActionResultDTO<ActivityResponseDTO>> GetActivityResponseByIdAsync(int id)
    {
        var activityResult = await GetActivityByIdAsync(id);

        if (activityResult.Data is null || !activityResult.Success)
            return ActionResultDTO<ActivityResponseDTO>.Fail(ErrorConstants.NotFoundActivityError, ErrorType.NotFound);

        return ActionResultDTO<ActivityResponseDTO>.Ok(activityResult.Data.ToDto());
    }

    public async Task<ActionResultDTO<ActivityResponseDTO>> CreateActivityAsync(ActivityRequestDTO activityDTO)
    {
        var goalExists = await _goalService.VerifyExistsGoalAsync(activityDTO.GoalId);

        if (!goalExists)
            return ActionResultDTO<ActivityResponseDTO>.Fail(ErrorConstants.GoalExistanceError, ErrorType.NotFound);

        var user = _userService.GetUserName() ?? "Unknown";

        var activity = activityDTO.ToEntity(user);

        try
        {
            var create = await _activityRepository.AddAsync(activity);

            return ActionResultDTO<ActivityResponseDTO>.Ok(create.ToDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LogConstants.CreateActivityError);
            return ActionResultDTO<ActivityResponseDTO>.Fail($"{ErrorConstants.CreationError}, {ex.Message}", ErrorType.Unknown);
        }
    }

    public async Task<ActionResultDTO<ActivityResponseDTO>> UpdateActivityAsync(ActivityRequestDTO activityDTO)
    {
        var goalExists = await _goalService.VerifyExistsGoalAsync(activityDTO.GoalId);

        if (!goalExists)
            return ActionResultDTO<ActivityResponseDTO>.Fail(ErrorConstants.GoalExistanceError, ErrorType.NotFound);

        var activityResult = await GetActivityByIdAsync(activityDTO.Id);

        if (activityResult.Data is null || !activityResult.Success)
            return ActionResultDTO<ActivityResponseDTO>.Fail(ErrorConstants.NotFoundActivityError, ErrorType.NotFound);

        var user = _userService.GetUserName() ?? "Unknown";

        activityResult.Data.Update(
            activityDTO.GoalId,
            activityDTO.Title,
            activityDTO.Description,
            user
        );

        try
        {
            var updated = await _activityRepository.UpdateAsync(activityResult.Data);

            return ActionResultDTO<ActivityResponseDTO>.Ok(updated.ToDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LogConstants.UpdateActivityError, activityDTO.Id);
            return ActionResultDTO<ActivityResponseDTO>.Fail($"{ErrorConstants.UpdateError}, {ex.Message}", ErrorType.Unknown);
        }
    }

    public async Task<ActionResultDTO<bool>> DeleteActivityAsync(int id)
    {
        var activity = await _activityRepository.GetActivityById(id).FirstOrDefaultAsync();

        if (activity is null)
            return ActionResultDTO<bool>.Fail(ErrorConstants.NotFoundActivityError, ErrorType.NotFound);

        try
        {
            var result = await _activityRepository.RemoveAsync(activity);

            return result
               ? ActionResultDTO<bool>.Ok(true)
               : ActionResultDTO<bool>.Fail(ErrorConstants.DeletionError, ErrorType.NotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LogConstants.DeleteActivityError, id);
            return ActionResultDTO<bool>.Fail($"{ErrorConstants.DeletionError}, {ex.Message}", ErrorType.Unknown);
        }
    }

    private async Task<ActionResultDTO<ActivityModel>> GetActivityByIdAsync(int id)
    {
        var activityResult = await _activityRepository.GetActivityById(id).FirstOrDefaultAsync();

        if (activityResult is null)
            return ActionResultDTO<ActivityModel>.Fail(ErrorConstants.NotFoundGoalError, ErrorType.NotFound);

        return ActionResultDTO<ActivityModel>.Ok(activityResult);
    }
}
