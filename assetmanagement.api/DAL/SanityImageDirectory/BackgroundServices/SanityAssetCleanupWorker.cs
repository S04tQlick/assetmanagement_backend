using AssetManagement.API.DAL.SanityImageDirectory.Repositories;

namespace AssetManagement.API.DAL.SanityImageDirectory.BackgroundServices;

// public class SanityAssetCleanupWorker(ISanityImageRepository repo, ILogger<SanityAssetCleanupWorker> log)
//     : BackgroundService
// {
//     private readonly ISanityImageRepository _repo = repo;
//     private readonly TimeSpan _interval = TimeSpan.FromHours(6);
//
//     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//     {
//         while (!stoppingToken.IsCancellationRequested)
//         {
//             log.LogInformation("Running Sanity orphan asset cleanup...");
//
//             // SEARCH all assets here (optional I can add query logic next if needed)
//             // foreach(var asset in assets) check count => delete
//
//             await Task.Delay(_interval, stoppingToken);
//         }
//     }
// }


public class SanityAssetCleanupWorker(IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<ISanityImageRepository>();

            // Example cleanup: delete assets with no references
            var candidateAssets = new[] { "asset-id-1", "asset-id-2" }; // you’d fetch these
            foreach (var assetId in candidateAssets)
            {
                var count = await repo.GetAssetReferenceCountAsync(assetId);
                if (count == 0)
                {
                    await repo.DeleteAssetAsync(assetId);
                }
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
        
        
        
        
        // while (!stoppingToken.IsCancellationRequested)
        // {
        //     using var scope = scopeFactory.CreateScope();
        //     var repo = scope.ServiceProvider.GetRequiredService<ISanityImageRepository>();
        //
        //     // Perform cleanup work
        //     await repo.CleanupAsync(stoppingToken);
        //
        //     // Wait before next run
        //     await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        // }
    }
}
