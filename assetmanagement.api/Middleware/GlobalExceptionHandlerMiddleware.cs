using System.Net; 
using AssetManagement.API.Exceptions; 

namespace AssetManagement.API.Middleware;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred while processing request: {Path}", context.Request.Path);
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, errorMessage) = exception switch
        {
            ValidationException => (HttpStatusCode.BadRequest, exception.Message),
            NotFoundException   => (HttpStatusCode.NotFound, exception.Message),
            ConflictException   => (HttpStatusCode.Conflict, exception.Message), 
            DomainException     => (HttpStatusCode.BadRequest, exception.Message),
            _                   => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
        };

        context.Response.StatusCode = (int)statusCode;

        var errorResponse = new
        {
            StatusCode = context.Response.StatusCode,
            Error = errorMessage
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}