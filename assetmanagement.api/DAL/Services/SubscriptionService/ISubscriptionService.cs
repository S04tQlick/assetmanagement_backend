namespace AssetManagement.API.DAL.Services.SubscriptionService;

public interface ISubscriptionService
{
    Task CheckSubscriptionsAsync();
    Task<bool> RenewSubscriptionAsync(Guid institutionId, int months);
}