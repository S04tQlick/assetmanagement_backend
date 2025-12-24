namespace AssetManagement.API.DAL.Services.RedisPublisher;

public interface IRedisPublisher
{
    Task PublishAsync(string channel, object message);
}