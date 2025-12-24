using AssetManagement.API.DAL.Services.SubscriptionService;
using Serilog;

namespace AssetManagement.API.DAL.Services.BackgroundServices;

public class SubscriptionBackgroundService(IServiceProvider provider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Log.Information("Subscription background service started at {Time}", DateTime.UtcNow);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = provider.CreateScope();
                var subService = scope.ServiceProvider.GetRequiredService<ISubscriptionService>();

                await subService.CheckSubscriptionsAsync();
                Log.Information("Subscription check completed at {Time}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred during subscription check");
            }

            // Delay for 24 hours before the next check
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }

        Log.Information("Subscription background service stopped at {Time}", DateTime.UtcNow);
    }
}