using Microsoft.EntityFrameworkCore;
using PACE.Data;
using PACE.Interfaces.IRepositories;
using PACE.Models.EvaluationModels;

namespace PACE.Repositories;

public class EvaluationRepository(PaceDbContext context) : IEvaluationRepository
{
    private readonly PaceDbContext _context = context;

    public IQueryable<EvaluationModel> GetEvaluations()
    {
        return _context.Evaluations
            .Where(e => e.IsActive);
    }

    public IQueryable<EvaluationModel> GetEvaluationById(int id)
    {
        return _context.Evaluations
            .AsNoTracking()
            .Where(e => e.IsActive && e.Id == id)
            .Include(e => e.Employee);
    }

    public IQueryable<EvaluationModel> GetEvaluationByEmployeeId(int EmployeeId, int year)
    {
        return _context.Evaluations
            .AsNoTracking()
            .Where(e => e.IsActive && e.EmployeeId == EmployeeId && e.Year == year)
            .Include(e => e.Employee);
    }

    public IQueryable<EvaluationModel> GetEvaluationsWithGoalsByEmployeeId(int employeeId, int year)
    {
        return _context.Evaluations
            .AsNoTracking()
            .Include(e => e.Goals)
            .Include(e => e.Employee)
            .Where(e => e.IsActive && e.EmployeeId == employeeId && e.Year == year);
    }

    public async Task<bool> AddRangeAsync(IEnumerable<EvaluationModel> evaluations)
    {
        await _context.Evaluations.AddRangeAsync(evaluations);
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<EvaluationModel> UpdateAsync(EvaluationModel evaluation)
    {
        _context.Evaluations.Update(evaluation);
        await _context.SaveChangesAsync();

        return evaluation;
    }

    public async Task<bool> EvaluationExistsByIdAsync(int id)
    {
        var evaluationExists = await _context.Evaluations
            .AsNoTracking()
            .AnyAsync(e => e.IsActive && e.Id == id);

        return evaluationExists;
    }

    public async Task<bool> EvaluationExistsByEmployeeIdAsync(int employeeId, int year)
    {
        var evaluationExists = await _context.Evaluations
            .AsNoTracking()
            .AnyAsync(e => e.IsActive && e.EmployeeId == employeeId && e.Year == year);

        return evaluationExists;
    }

    public IQueryable<EvaluationModel> AllEvaluationsExistsByDepartmentIdIdAsync(int departmentId, int year)
    {
        var evaluationExists = _context.Evaluations
            .AsNoTracking()
            .Include(e => e.Employee)
            .Where(e =>
                e.IsActive &&
                e.Employee != null &&
                e.Employee.IsActive &&
                e.Employee.DepartmentId == departmentId &&
                e.Year == year &&
                e.Employee.Id == e.EmployeeId);

        return evaluationExists;
    }
}
