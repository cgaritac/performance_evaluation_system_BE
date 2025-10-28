using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PACE.Interfaces.IRepositories;
using PACE.Interfaces.IServices;
using PACE.Models.CommonModels;
using PACE.Models.GoalModels;
using PACE.Utils.Constants;
using PACE.Utils.Enums;
using PACE.Utils.Helpers;
using PACE.Utils.Mappers;

namespace PACE.Services;

public class GoalService(IGoalRepository goalRepository, IEvaluationService evaluationService, ILogger<GoalService> logger, IUserService userService) : IGoalService
{
    private readonly IGoalRepository _goalRepository = goalRepository;
    private readonly IEvaluationService _evaluationService = evaluationService;
    private readonly ILogger<GoalService> _logger = logger;
    private readonly IUserService _userService = userService;

    public async Task<ActionResultDTO<GoalResponseDTO>> GetGoalResponseByIdAsync(int id)
    {
        var goalResult = await GetGoalByIdAsync(id);

        if (goalResult.Data is null || !goalResult.Success)
            return ActionResultDTO<GoalResponseDTO>.Fail(ErrorConstants.NotFoundGoalError, ErrorType.NotFound);

        return ActionResultDTO<GoalResponseDTO>.Ok(goalResult.Data.ToDto());
    }

    public async Task<ActionResultDTO<GoalResponseDTO>> CreateGoalAsync(GoalRequestDTO goalDTO)
    {
        var datesValidation = ValidatorHelper.ValidateGoalDates(goalDTO.StartDate, goalDTO.EndDate, goalDTO.DueDate);

        if (datesValidation != null)
            return ActionResultDTO<GoalResponseDTO>.Fail(datesValidation, ErrorType.Validation);

        var evaluationExists = await _evaluationService.VerifyEvaluationExistsByIdAsync(goalDTO.EvaluationId);

        if (!evaluationExists)
            return ActionResultDTO<GoalResponseDTO>.Fail(ErrorConstants.EvaluationExistanceError, ErrorType.NotFound);

        var user = _userService.GetUserName() ?? "Unknown";

        var goal = goalDTO.ToEntity(user);

        try
        {
            var created = await _goalRepository.AddAsync(goal);

            return ActionResultDTO<GoalResponseDTO>.Ok(created.ToDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LogConstants.CreateGoalError, goalDTO.EvaluationId);
            return ActionResultDTO<GoalResponseDTO>.Fail($"{ErrorConstants.CreationError}, {ex.Message}", ErrorType.Unknown);
        }
    }

    public async Task<ActionResultDTO<GoalResponseDTO>> UpdateGoalAsync(GoalRequestDTO goalDTO)
    {
        var datesValidation = ValidatorHelper.ValidateGoalDates(goalDTO.StartDate, goalDTO.EndDate, goalDTO.DueDate);

        if (datesValidation != null)
            return ActionResultDTO<GoalResponseDTO>.Fail(datesValidation, ErrorType.Validation);

        var evaluationExists = await _evaluationService.VerifyEvaluationExistsByIdAsync(goalDTO.EvaluationId);

        if (!evaluationExists)
            return ActionResultDTO<GoalResponseDTO>.Fail(ErrorConstants.EvaluationExistanceError, ErrorType.NotFound);

        var goalResult = await GetGoalByIdAsync(goalDTO.Id);

        if (goalResult.Data is null || !goalResult.Success)
            return ActionResultDTO<GoalResponseDTO>.Fail(ErrorConstants.NotFoundGoalError, ErrorType.NotFound);

        var isStatusChange = VerifyIfIsStatusChange(goalResult.Data.Status, goalDTO.Status);
        var isAdmin = _userService.GetUserRole() == RolConstants.AdminRol;

        if (!isStatusChange && goalResult.Data.GoalType == GoalTypeEnum.ManagerAssigned && !isAdmin)
            return ActionResultDTO<GoalResponseDTO>.Fail(ErrorConstants.UserNotAllowedError, ErrorType.Validation);        

        goalDTO = ChangeStatus(goalDTO);

        var user = _userService.GetUserName() ?? "Unknown";

        goalResult.Data.Update(
            goalDTO.EvaluationId,
            goalDTO.GoalCategory,
            goalDTO.GoalType,
            goalDTO.Title,
            goalDTO.StartDate,
            goalDTO.EndDate,
            goalDTO.DueDate,
            goalDTO.Approval,
            goalDTO.Description,
            goalDTO.Status,
            user
        );

        try
        {
            var updated = await _goalRepository.UpdateAsync(goalResult.Data);

            return ActionResultDTO<GoalResponseDTO>.Ok(updated.ToDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LogConstants.UpdateGoalError, goalDTO.Id);
            return ActionResultDTO<GoalResponseDTO>.Fail($"{ErrorConstants.UpdateError}, {ex.Message}", ErrorType.Unknown);
        }
    }

    public async Task<ActionResultDTO<bool>> DeleteGoalAsync(int id)
    {
        var goalResponseResult = await GetGoalByIdAsync(id);

        if (goalResponseResult.Data is null || !goalResponseResult.Success)
            return ActionResultDTO<bool>.Fail(ErrorConstants.NotFoundGoalError, ErrorType.NotFound);

        var isAdmin = _userService.GetUserRole() == RolConstants.AdminRol;

        if (goalResponseResult.Data.GoalType == GoalTypeEnum.ManagerAssigned && !isAdmin)
            return ActionResultDTO<bool>.Fail(ErrorConstants.UserNotAllowedError, ErrorType.Validation);

        try
        {
            var result = await _goalRepository.RemoveAsync(goalResponseResult.Data);

            return result
                ? ActionResultDTO<bool>.Ok(true)
                : ActionResultDTO<bool>.Fail(ErrorConstants.DeletionError, ErrorType.NotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LogConstants.DeleteGoalError, id);
            return ActionResultDTO<bool>.Fail($"{ErrorConstants.DeletionError}, {ex.Message}", ErrorType.Unknown);
        }
    }

    public async Task<bool> VerifyExistsGoalAsync(int id)
    {
        return await _goalRepository.ExistsGoalAsync(id);
    }

    private async Task<ActionResultDTO<GoalModel>> GetGoalByIdAsync(int id)
    {
        var goal = await _goalRepository.GetGoalById(id).FirstOrDefaultAsync();

        if (goal is null)
            return ActionResultDTO<GoalModel>.Fail(ErrorConstants.NotFoundGoalError, ErrorType.NotFound);

        return ActionResultDTO<GoalModel>.Ok(goal);
    }

    private bool VerifyIfIsStatusChange(GoalStatusEnum oldStatud, GoalStatusEnum newStatud)
    {
        if (oldStatud == newStatud)
            return false;

        return true;
    }

    private GoalRequestDTO ChangeStatus(GoalRequestDTO goalDTO)
    {
        if (goalDTO.Status == GoalStatusEnum.Cancelled || goalDTO.Status == GoalStatusEnum.OnHold)
            return goalDTO;

        if (goalDTO.StartDate == null && goalDTO.Status == GoalStatusEnum.InProgress)
        {
            goalDTO.StartDate = DateTime.UtcNow;
            return goalDTO;
        }

        if (goalDTO.StartDate != null && goalDTO.Status == GoalStatusEnum.InProgress)
        {
            return goalDTO;
        }

        if (goalDTO.StartDate != null && goalDTO.EndDate == null && goalDTO.Status == GoalStatusEnum.Finished)
        {
            goalDTO.EndDate = DateTime.UtcNow;

            goalDTO.Status = goalDTO.EndDate.Value.Date <= goalDTO.DueDate.Date
                            ? GoalStatusEnum.Finished
                            : GoalStatusEnum.Delayed;

            return goalDTO;
        }

        return goalDTO;
    }
}