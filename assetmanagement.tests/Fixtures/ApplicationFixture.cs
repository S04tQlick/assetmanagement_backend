using Microsoft.Extensions.Hosting;

namespace AssetManagement.Tests.Fixtures;

public class ApplicationFixture : IAsyncLifetime
{
    public HttpClient Client { get; }

    public ApplicationFixture()
    {
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var hostedServices = services
                        .Where(s => typeof(IHostedService).IsAssignableFrom(s.ServiceType))
                        .ToList();

                    foreach (var hostedService in hostedServices)
                        services.Remove(hostedService);
                });
            });

        Client = factory.CreateClient();
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public Task DisposeAsync() => Task.CompletedTask;
}