using Serilog.Context;

namespace AssetManagement.API.Middleware;

public class LoggingContextMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var userId = context.User?.FindFirst("sub")?.Value;
        var institutionId = context.Items.TryGetValue("InstitutionId", out var inst) ? inst?.ToString() : null;

        using (LogContext.PushProperty("UserId", userId ?? "anonymous"))
        using (LogContext.PushProperty("InstitutionId", institutionId ?? "none"))
        using (LogContext.PushProperty("RequestPath", context.Request.Path))
        {
            await next(context);
        }
    }
}

public static class LoggingContextMiddlewareExtensions
{
    public static IApplicationBuilder UseLoggingContext(this IApplicationBuilder app)
    {
        return app.UseMiddleware<LoggingContextMiddleware>();
    }
}