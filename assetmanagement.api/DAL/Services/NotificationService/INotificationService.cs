using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Services.NotificationService;

public interface INotificationService
{
    Task SendSubscriptionReminderAsync(InstitutionsModel institutionsModel);
    Task SendSubscriptionExpiredAsync(InstitutionsModel institutionsModel);
    Task SendMaintenanceDueAsync(AssetsModel assetsModel);
}