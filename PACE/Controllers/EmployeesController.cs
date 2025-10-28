using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PACE.Interfaces.IServices;
using PACE.Utils.Helpers;

namespace PACE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(IEmployeeService employeeService) : ControllerBase
{
    private readonly IEmployeeService _employeeService = employeeService;

    [HttpGet("iddepartment/{departmentId}")]
    public async Task<IActionResult> GetByDepartment(int departmentId)
    {
        var result = await _employeeService.GetEmployeesAsync(departmentId);

        return ControllerResponseHelper.HandleControllerResult(result);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await _employeeService.GetEmployeeResponseByEmailAsync(email);

        return ControllerResponseHelper.HandleControllerResult(result);
    }
}
