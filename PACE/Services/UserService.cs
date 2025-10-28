using PACE.Interfaces.IServices;

namespace PACE.Services;

public class UserService(IHttpContextAccessor accessor) : IUserService
{
    private readonly IHttpContextAccessor _accessor = accessor;

    public string? GetUserName()
    {
        return _accessor.HttpContext?.User?.Identity?.Name ?? "Unknown";
    }

    public string? GetUserRole()
    {
        return _accessor.HttpContext?.User?.IsInRole("Admin") == true ? "Admin" :
               _accessor.HttpContext?.User?.IsInRole("Manager") == true ? "User" : "Unknown";
    }
}