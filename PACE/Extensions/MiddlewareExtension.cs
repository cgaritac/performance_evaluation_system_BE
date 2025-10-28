using PACE.Middleware;

namespace PACE.Extensions;

public static class MiddlewareExtension
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }

    public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CorsMiddleware>();
    }
}
