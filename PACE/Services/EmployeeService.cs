using Microsoft.EntityFrameworkCore;
using PACE.Interfaces.IRepositories;
using PACE.Interfaces.IServices;
using PACE.Models.CommonModels;
using PACE.Models.EmployeeModels;
using PACE.Utils.Constants;
using PACE.Utils.Enums;
using PACE.Utils.Mappers;

namespace PACE.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<ActionResultDTO<List<EmployeeDTO>>> GetEmployeesAsync(int departmentId)
    {
        var employees = await _employeeRepository.GetEmployeesByDepartmentAsync(departmentId).ToListAsync();
        var employeeDTOs = employees.Select(e => e.ToDto()).ToList();

        return ActionResultDTO<List<EmployeeDTO>>.Ok(employeeDTOs);
    }

    public async Task<ActionResultDTO<EmployeeDTO>> GetEmployeeResponseByEmailAsync(string email)
    {
        var employee = await _employeeRepository.GetEmployeeByEmailAsync(email).FirstOrDefaultAsync();

        if (employee is null)
            return ActionResultDTO<EmployeeDTO>.Fail(ErrorConstants.NotFoundEmployeeError, ErrorType.NotFound);

        return ActionResultDTO<EmployeeDTO>.Ok(employee.ToDto());
    }

    public async Task<bool> VerifyExistsEmployeeAsync(int id)
    {
        return await _employeeRepository.ExistsEmployeeAsync(id);
    }
}
