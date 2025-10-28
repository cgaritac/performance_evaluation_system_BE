using PACE.Models.EmployeeModels;
using PACE.Utils.Helpers;

namespace PACE.Utils.Mappers;

public static class EmployeeMapper
{
    public static EmployeeDTO ToDto(this EmployeeModel employee) => new(
        employee.Id,
        FullNameHelper.GetFullName(employee.FirstName, employee.LastName),
        employee.EmployeeEmail,
        employee.DepartmentId
    );
}
