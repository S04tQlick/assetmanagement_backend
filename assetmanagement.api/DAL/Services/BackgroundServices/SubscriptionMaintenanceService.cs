using AssetManagement.API.DAL.Repositories.AssetsRepository;
using AssetManagement.API.DAL.Repositories.InstitutionsRepository;
using AssetManagement.API.DAL.Services.DepreciationService;
using AssetManagement.API.DAL.Services.NotificationService;
using Serilog;

namespace AssetManagement.API.DAL.Services.BackgroundServices;

public class SubscriptionMaintenanceService(IServiceProvider serviceProvider) : BackgroundService {
    private readonly TimeSpan _interval = TimeSpan.FromHours(6);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Log.Information("Subscription & Maintenance Background Service started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            var institutionRepo = scope.ServiceProvider.GetRequiredService<IInstitutionRepository>();
            var assetRepo = scope.ServiceProvider.GetRequiredService<IAssetRepository>();
            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
            var depreciationService = scope.ServiceProvider.GetRequiredService<IDepreciationService>();

            await HandleSubscriptionChecks(institutionRepo, notificationService);
            await HandleAssetMaintenance(assetRepo, notificationService, depreciationService);

            await Task.Delay(_interval, stoppingToken);
        }
    }

    private async Task HandleSubscriptionChecks(IInstitutionRepository institutionRepo,
        INotificationService notificationService)
    {
        var institutions = await institutionRepo.GetAllAsync();
        var now = DateTime.UtcNow;

        foreach (var institution in institutions)
        {
            try
            {
                if (institution is { IsActive: false, Subscription: not null } && institution.Subscription.SubscriptionEndDate < now)
                    continue;

                if (institution.Subscription != null)
                {
                    var daysToExpiry = (institution.Subscription.SubscriptionEndDate - now).TotalDays;

                    if (daysToExpiry is <= 7 and > 0)
                    {
                        await notificationService.SendSubscriptionReminderAsync(institution);
                    }
                }

                if (institution.Subscription == null || institution.Subscription.SubscriptionEndDate > now) continue;
                institution.IsActive = false;
                await institutionRepo.UpdateAsync(institution);
                await notificationService.SendSubscriptionExpiredAsync(institution);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error processing institution {InstitutionId}", institution.Id);
            }
        }
    }

    private async Task HandleAssetMaintenance(IAssetRepository assetRepo,
        INotificationService notificationService, IDepreciationService depreciationService)
    {
        var assets = await assetRepo.GetAllAsync();

        foreach (var asset in assets)
        {
            try
            {
                await depreciationService.UpdateDepreciationValuesAsync(asset);

                if (asset.NextMaintenanceDate <= DateTime.UtcNow)
                {
                    await notificationService.SendMaintenanceDueAsync(asset);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error processing asset {AssetId}", asset.Id);
            }
        }
    }
}
