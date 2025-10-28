using PACE.Models.CommonModels;
using PACE.Utils.Constants;
using System.Text.Json;

namespace PACE.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;
    private readonly IWebHostEnvironment _env = env;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LogConstants.UnhandledExceptionError, context.Request.Path, context.Request.Method);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var errorResponse = new ErrorResponseDTO { ErrorMessage = ErrorConstants.UnexpectedError };

        if (_env.IsDevelopment() || _env.IsEnvironment("Localhost"))
        {
            errorResponse.Exception = ex.Message;
            errorResponse.StackTrace = ex.StackTrace;
        }

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var json = JsonSerializer.Serialize(errorResponse, options);

        await context.Response.WriteAsync(json);
    }
}
