using System.Net;
using System.Text.Json;
using Serilog;

namespace AssetManagement.API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unhandled exception occurred");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorPayload = new
            {
                error = "An unexpected error occurred.",
                message = ex.Message,
                statusCode = context.Response.StatusCode
            };

            var json = JsonSerializer.Serialize(errorPayload);
            await context.Response.WriteAsync(json);
        }
    }
}

// Extension method for easy registration
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}





















// public class ExceptionHandlingMiddleware(RequestDelegate next)
// {
//     public async Task InvokeAsync(HttpContext context)
//     {
//         try
//         {
//             await next(context);
//         }
//         catch (Exception ex)
//         {
//             Log.Error(ex, "An unhandled exception occurred: {Message}", ex.Message);
//             await HandleExceptionAsync(context, ex);
//         }
//     }
//
//     private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
//     {
//         var response = context.Response;
//         response.ContentType = "application/json";
//
//         var statusCode = exception switch
//         {
//             UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
//             ArgumentException => (int)HttpStatusCode.BadRequest,
//             KeyNotFoundException => (int)HttpStatusCode.NotFound,
//             _ => (int)HttpStatusCode.InternalServerError
//         };
//
//         var error = new ErrorResponse
//         {
//             StatusCode = statusCode,
//             Message = exception.Message,
//             Details = exception.InnerException?.Message,
//             Path = context.Request.Path
//         };
//
//         response.StatusCode = statusCode;
//         var json = JsonSerializer.Serialize(error, new JsonSerializerOptions { WriteIndented = true });
//         await response.WriteAsync(json);
//     }
// }
//
//
// public static class ExceptionHandlingMiddlewareExtensions
// {
//     public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
//     {
//         return builder.UseMiddleware<ExceptionHandlingMiddleware>();
//     }
// }