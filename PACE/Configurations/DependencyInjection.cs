using PACE.Interceptors;
using PACE.Interfaces.IRepositories;
using PACE.Interfaces.IServices;
using PACE.Repositories;
using PACE.Services;

namespace PACE.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEvaluationRepository, EvaluationRepository>();
        services.AddScoped<IGoalRepository, GoalRepository>();
        services.AddScoped<IActivityRepository, ActivityRepository>();

        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IEvaluationService, EvaluationService>();
        services.AddScoped<IGoalService, GoalService>();
        services.AddScoped<IActivityService, ActivityService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<SoftDeleteInterceptor>();

        services.AddHttpContextAccessor();

        return services;
    }

}
