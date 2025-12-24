namespace AssetManagement.API.Extentions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<Middleware.GlobalExceptionHandlerMiddleware>();
    }
}