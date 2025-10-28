using PACE.Models.CommonModels;
using PACE.Models.GoalModels;

namespace PACE.Interfaces.IServices;

public interface IGoalService
{
    Task<ActionResultDTO<GoalResponseDTO>> GetGoalResponseByIdAsync(int id);
    Task<ActionResultDTO<GoalResponseDTO>> CreateGoalAsync(GoalRequestDTO goalDTO);
    Task<ActionResultDTO<GoalResponseDTO>> UpdateGoalAsync(GoalRequestDTO goalDTO);
    Task<ActionResultDTO<bool>> DeleteGoalAsync(int id);
    Task<bool> VerifyExistsGoalAsync(int id);
}
