using Microsoft.EntityFrameworkCore;
using PACE.Interceptors;
using PACE.Extensions;

namespace PACE.Configurations;

public static class ServiceConfiguration
{
    public static WebApplicationBuilder AddDefaultServices<T>(this WebApplicationBuilder builder) where T : DbContext
    {
        var dbConnectionString = builder.Configuration.GetConnectionString("AppConnection");

        builder.Services.AddDbContextPool<T>((serviceProvider, options) =>
        {
            var interceptor = serviceProvider.GetRequiredService<SoftDeleteInterceptor>();

            options.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString))
                   .AddInterceptors(interceptor);
        });

        builder.Services.AddMicrosoftEntraAuthentication(builder.Configuration);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerWithAuth();

        return builder;
    }
}