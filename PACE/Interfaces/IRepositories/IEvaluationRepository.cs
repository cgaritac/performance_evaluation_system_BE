using PACE.Models.EvaluationModels;

namespace PACE.Interfaces.IRepositories;

public interface IEvaluationRepository
{
    IQueryable<EvaluationModel> GetEvaluations();
    IQueryable<EvaluationModel> GetEvaluationById(int id);
    IQueryable<EvaluationModel> GetEvaluationByEmployeeId(int employeeId, int year);
    IQueryable<EvaluationModel> GetEvaluationsWithGoalsByEmployeeId(int employeeId, int year);
    Task<bool> AddRangeAsync(IEnumerable<EvaluationModel> evaluations);
    Task<EvaluationModel> UpdateAsync(EvaluationModel evaluation);
    Task<bool> EvaluationExistsByIdAsync(int id);
    Task<bool> EvaluationExistsByEmployeeIdAsync(int employeeId, int year);
    IQueryable<EvaluationModel> AllEvaluationsExistsByDepartmentIdAsync(int departmentId, int year);
}
