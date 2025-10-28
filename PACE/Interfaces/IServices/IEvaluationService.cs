using PACE.Models.CommonModels;
using PACE.Models.EvaluationModels;

namespace PACE.Interfaces.IServices;

public interface IEvaluationService
{
    Task<ActionResultDTO<PageResponseDTO<EvaluationResponseDTO>>> GetEvaluations_PagedAsync(PageRequestDTO pageRequest);
    Task<ActionResultDTO<EvaluationWithGoalsResponseDTO>> GetEvaluationWithGoalsByEmployeeIdAsync(int employeeId, int year);
    Task<ActionResultDTO<bool>> CreateEvaluationsAsync(EvaluationRequestDTO evaluationDTO);
    Task<ActionResultDTO<EvaluationResponseDTO>> UpdateEvaluationAsync(EvaluationRequestDTO evaluationDTO);
    Task<bool> VerifyEvaluationExistsByIdAsync(int id);
}
