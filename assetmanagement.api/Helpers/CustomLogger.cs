using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace AssetManagement.API.Helpers;

public abstract class CustomLogger
{
    public static Logger CreateLogger()=> 
        new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("System", LogEventLevel.Warning)
        .MinimumLevel.Information()
        .Enrich.FromLogContext()
        .Enrich.WithThreadId()
        .Enrich.WithEnvironmentName()
        .Enrich.WithProcessId()
        .WriteTo.Async(a => a.Console(
            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} (at {SourceContext}){NewLine}{Exception}"
        ))
        .WriteTo.Async(a => a.File(
            path: "logs/app-.log",
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 14,
            shared: true,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} (User: {UserId}, Inst: {InstitutionId}){NewLine}{Exception}"
        ))
        .CreateLogger();
}