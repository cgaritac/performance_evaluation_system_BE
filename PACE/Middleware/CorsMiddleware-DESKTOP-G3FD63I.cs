namespace PACE.Middleware;

public class CorsMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public Task Invoke(HttpContext context)
    {
        return BeginInvoke(context);
    }


    public async Task BeginInvoke(HttpContext context)
    {
        context.Response.Headers.Append("Access-Control-Allow-Origin", new[] { "*" });
        context.Response.Headers.Append("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS" });
        context.Response.Headers.Append("Access-Control-Allow-Headers", new[] { "Origin, X-Requested-With, Content-Type, Accept, Authorization" });

        if (context.Request.Method == HttpMethods.Options)
        {
            context.Response.StatusCode = StatusCodes.Status204NoContent;
            return;
        }

        await _next.Invoke(context);
    }
}
