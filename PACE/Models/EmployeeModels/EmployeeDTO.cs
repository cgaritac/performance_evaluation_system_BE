namespace PACE.Models.EmployeeModels;

public class EmployeeDTO
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public int DepartmentId { get; set; }

    public EmployeeDTO()
    {
        Id = 0;
        FullName = string.Empty;
        Email = string.Empty;
        DepartmentId = 0;
    }

    public EmployeeDTO(int id, string fullName, string email, int departmentId)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        DepartmentId = departmentId;
    }
}
