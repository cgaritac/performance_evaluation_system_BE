using Microsoft.EntityFrameworkCore;
using PACE.Data;
using PACE.Interfaces.IRepositories;
using PACE.Models.EmployeeModels;

namespace PACE.Repositories;

public class EmployeeRepository(PaceDbContext context) : IEmployeeRepository
{
    private readonly PaceDbContext _context = context;

    public IQueryable<EmployeeModel> GetEmployeesByDepartmentAsync(int departmentId)
    {
        return _context.Employees
            .AsNoTracking()
            .Where(e => e.IsActive && e.DepartmentId == departmentId);
    }

    public IQueryable<EmployeeModel> GetEmployeeByEmailAsync(string email)
    {
        return _context.Employees
            .AsNoTracking()
            .Where(e => e.IsActive && e.EmployeeEmail == email);
    }

    public async Task<bool> ExistsEmployeeAsync(int id)
    {
        var employeeExists = await _context.Employees
            .AsNoTracking()
            .AnyAsync(e => e.IsActive && e.Id == id);

        return employeeExists;
    }
}
