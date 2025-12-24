using AssetManagement.API.Middleware;
using Serilog;

namespace AssetManagement.API.Extentions.ProgramExtention;

public static class AppServiceExtentionCollection
{
    public static void AppBuilderAddUsings(this WebApplicationBuilder builder)
    {
        var app = builder.Build();

        app.UseLoggingContext();
        app.UseGlobalExceptionHandler();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseMiddleware<InstitutionContextMiddleware>();
        
        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
        
        try
        {
            Log.Information("🚀 Starting AssetManagement API...");
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "❌ Application startup failed!");
        }
        finally
        {
            Log.Information("💤 Application shutting down...");
            Log.CloseAndFlush();
        }
    }
}