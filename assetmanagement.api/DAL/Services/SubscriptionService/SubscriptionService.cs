using AssetManagement.API.DAL.DatabaseContext;
using AssetManagement.Entities.Models;
using Serilog;
using StackExchange.Redis;

namespace AssetManagement.API.DAL.Services.SubscriptionService;

public class SubscriptionService(
    ApplicationDbContext context, IConnectionMultiplexer redis)
    : ISubscriptionService
{
        public async Task CheckSubscriptionsAsync()
        {
            var now = DateTime.UtcNow;
            var institutions = await context.InstitutionsModel
                .Include(i => i.Subscription)
                .ToListAsync();

            foreach (var inst in institutions)
            {
                if (inst.Subscription == null) continue;

                var sub = inst.Subscription;
                if (!sub.IsActive) continue;

                
                if (sub.SubscriptionEndDate < now)
                {
                    Log.Warning("Subscription expired for Institution {InstitutionName} (ID: {InstitutionId})", inst.InstitutionName, inst.Id);
                    sub.IsActive = false;
                    inst.IsActive = false;
                    await DisableInstitutionUsersAsync(inst.Id);
                    await NotifyAsync($"Subscription for {inst.InstitutionName} has expired.");
                }
                
                else if ((sub.SubscriptionEndDate - now).TotalDays <= 7)
                {
                    Log.Information("Subscription expiring soon for Institution {InstitutionName}, end date: {EndDate}", inst.InstitutionName, sub.SubscriptionEndDate);
                    await NotifyAsync($"Subscription for {inst.InstitutionName} is expiring soon ({sub.SubscriptionEndDate:d}). Please renew.");
                }
            }

            await context.SaveChangesAsync();
        }
    
        public async Task<bool> RenewSubscriptionAsync(Guid institutionId, int months)
        {
            var inst = await context.InstitutionsModel
                .Include(i => i.Subscription)
                .FirstOrDefaultAsync(i => i.Id == institutionId);

            if (inst == null)
            {
                Log.Warning("Attempt to renew subscription for unknown institution ID: {InstitutionId}", institutionId);
                return false;
            }

            var now = DateTime.UtcNow;
            if (inst.Subscription == null)
            {
                inst.Subscription = new SubscriptionsModel
                {
                    Id = Guid.NewGuid(),
                    InstitutionId = institutionId,
                    SubscriptionStartDate = now,
                    SubscriptionEndDate = now.AddMonths(months),
                    IsActive = true
                };
                context.SubscriptionsModel.Add(inst.Subscription);
            }
            else
            {
                inst.Subscription.SubscriptionEndDate = inst.Subscription.SubscriptionEndDate < now
                    ? now.AddMonths(months)
                    : inst.Subscription.SubscriptionEndDate.AddMonths(months);
                inst.Subscription.IsActive = true;
                inst.IsActive = true;
            }

            await context.SaveChangesAsync();
            await NotifyAsync($"Subscription for {inst.InstitutionName} renewed until {inst.Subscription.SubscriptionEndDate:d}.");

            Log.Information("Subscription renewed for Institution {InstitutionName} until {EndDate}", inst.InstitutionName, inst.Subscription.SubscriptionEndDate);
            return true;
        }
        
        private async Task DisableInstitutionUsersAsync(Guid institutionId)
        {
            var users = await context.UsersModel
                .Where(iu => iu.InstitutionId == institutionId)
                .ToListAsync();

            foreach (var iu in users)
            {
                iu.IsActive = false;
            }

            await context.SaveChangesAsync();
            Log.Information("All users disabled for Institution ID: {InstitutionId}", institutionId);
        }
 
        private async Task NotifyAsync(string message)
        {
            var db = redis.GetDatabase();
            await db.PublishAsync(RedisChannel.Literal("notifications"), message);
            Log.Information("Notification published: {Message}", message);
        }
    }