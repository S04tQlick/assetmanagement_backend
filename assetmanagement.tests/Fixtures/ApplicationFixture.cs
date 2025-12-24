namespace AssetManagement.Tests.Fixtures;

public class ApplicationFixture : IAsyncLifetime
{
    public HttpClient Client { get; }
    
    public ApplicationFixture()
    {
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(_ =>
        {
        });
        
        Client = factory.CreateClient();
    }
    
    public Task InitializeAsync() => Task.FromResult(Task.CompletedTask);
    public Task DisposeAsync() => Task.FromResult(Task.CompletedTask);
}