using AssetManagement.API.DAL.Services.EmailService;
using AssetManagement.API.DAL.Services.RedisPublisher;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Services.NotificationService;

public class NotificationService(
    ILogger<NotificationService> logger,
    IRedisPublisher redisPublisher,
    IEmailService emailService) : INotificationService
{
    public async Task SendSubscriptionReminderAsync(InstitutionsModel institutionsModel)
    {
        const string subject = "Subscription Renewal Reminder";
        if (institutionsModel.Subscription != null)
        {
            var body = $@"
                <p>Dear {institutionsModel.InstitutionName},</p>
                <p>Your subscription will expire on <b>{institutionsModel.Subscription.SubscriptionEndDate:yyyy-MM-dd}</b>.</p>
                <p>Please renew soon to avoid service interruption.</p>
                <p>Regards,<br/>Asset Management System</p>";

            var message = new
            {
                type = "subscription_reminder",
                institutionId = institutionsModel.Id,
                institutionName = institutionsModel.InstitutionName,
                dueDate = institutionsModel.Subscription.SubscriptionEndDate,
                message = $"Subscription expires on {institutionsModel.Subscription.SubscriptionEndDate:yyyy-MM-dd}"
            };

            await redisPublisher.PublishAsync("notifications", message);

            if (!string.IsNullOrWhiteSpace(institutionsModel.InstitutionEmail))
                await emailService.SendEmailAsync(institutionsModel.InstitutionEmail, subject, body);
        }

        logger.LogInformation($"[Reminder] Subscription reminder sent for {institutionsModel.InstitutionName}");
    }

    public async Task SendSubscriptionExpiredAsync(InstitutionsModel institutionsModel)
    {
        const string subject = "Subscription Expired";
        if (institutionsModel.Subscription != null)
        {
            var body = $@"
                <p>Dear {institutionsModel.InstitutionName},</p>
                <p>Your subscription has expired on <b>{institutionsModel.Subscription.SubscriptionEndDate:yyyy-MM-dd}</b>.</p>
                <p>Your account has been temporarily disabled until renewal is completed.</p>
                <p>Regards,<br/>Asset Management System</p>";

            var message = new
            {
                type = "subscription_expired",
                institutionId = institutionsModel.Id,
                institutionName = institutionsModel.InstitutionName,
                message = "Your subscription has expired."
            };

            await redisPublisher.PublishAsync("notifications", message);

            if (!string.IsNullOrWhiteSpace(institutionsModel.InstitutionEmail))
                await emailService.SendEmailAsync(institutionsModel.InstitutionEmail, subject, body);
        }

        logger.LogWarning($"[Expired] Subscription expired for {institutionsModel.InstitutionName}");
    }

    public async Task SendMaintenanceDueAsync(AssetsModel assetsModel)
    {
        var subject = "Asset Maintenance Due";
        var body = $@"
                <p>Dear {assetsModel.Institutions?.InstitutionName},</p>
                <p>Asset <b>{assetsModel.AssetName}</b> is due for maintenance on <b>{assetsModel.NextMaintenanceDate:yyyy-MM-dd}</b>.</p>
                <p>Please ensure this asset is serviced promptly to avoid depreciation issues.</p>
                <p>Regards,<br/>Asset Management System</p>";

        var message = new
        {
            type = "maintenance_due",
            assetId = assetsModel.Id,
            assetName = assetsModel.AssetName,
            nextMaintenanceDate = assetsModel.NextMaintenanceDate,
            message = $"Asset '{assetsModel.AssetName}' requires maintenance."
        };

        await redisPublisher.PublishAsync("notifications", message);

        // Email institution if email available
        var institutionEmail = assetsModel.Institutions?.InstitutionEmail;
        if (!string.IsNullOrWhiteSpace(institutionEmail))
            await emailService.SendEmailAsync(institutionEmail, subject, body);

        logger.LogInformation($"[Maintenance] Maintenance due for {assetsModel.AssetName}");
    }
}