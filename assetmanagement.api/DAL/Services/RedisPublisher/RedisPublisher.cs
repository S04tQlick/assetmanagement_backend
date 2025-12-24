using System.Text.Json;
using StackExchange.Redis;

namespace AssetManagement.API.DAL.Services.RedisPublisher;

public class RedisPublisher(IConnectionMultiplexer redis, ILogger<RedisPublisher> logger) : IRedisPublisher
{
    public async Task PublishAsync(string channel, object message)
    {
        try
        {
            var db = redis.GetDatabase();
            var payload = JsonSerializer.Serialize(message);
            await db.PublishAsync( RedisChannel.Literal(channel), payload);
            logger.LogInformation("[Redis] Published message to channel: {Channel}", channel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "[Redis] Failed to publish message to {Channel}", channel);
        }
    }
}