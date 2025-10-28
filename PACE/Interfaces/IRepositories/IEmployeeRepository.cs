using PACE.Models.EmployeeModels;

namespace PACE.Interfaces.IRepositories;

public interface IEmployeeRepository
{
    IQueryable<EmployeeModel> GetEmployeesByDepartmentAsync(int departmentId);
    IQueryable<EmployeeModel> GetEmployeeByEmailAsync(string email);
    Task<bool> ExistsEmployeeAsync(int id);
}
