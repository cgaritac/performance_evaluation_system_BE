using PACE.Models.CommonModels;
using PACE.Models.EmployeeModels;

namespace PACE.Interfaces.IServices;

public interface IEmployeeService
{
    Task<ActionResultDTO<List<EmployeeDTO>>> GetEmployeesAsync(int departmentId);
    Task<ActionResultDTO<EmployeeDTO>> GetEmployeeResponseByEmailAsync(string email);
    Task<bool> VerifyExistsEmployeeAsync(int id);
}
