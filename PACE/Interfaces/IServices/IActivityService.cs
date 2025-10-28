using PACE.Models.ActivityModels;
using PACE.Models.CommonModels;

namespace PACE.Interfaces.IServices;

public interface IActivityService
{
    Task<ActionResultDTO<List<ActivityResponseDTO>>> GetActivitiesAsync(int goalId);
    Task<ActionResultDTO<ActivityResponseDTO>> GetActivityResponseByIdAsync(int id);
    Task<ActionResultDTO<ActivityResponseDTO>> CreateActivityAsync(ActivityRequestDTO activityDTO);
    Task<ActionResultDTO<ActivityResponseDTO>> UpdateActivityAsync(ActivityRequestDTO activityDTO);
    Task<ActionResultDTO<bool>> DeleteActivityAsync(int id);
}
