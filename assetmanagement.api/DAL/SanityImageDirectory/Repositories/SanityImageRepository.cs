using System.Net.Http.Headers;
using AssetManagement.API.DAL.SanityImageDirectory.BackgroundServices;
using AssetManagement.API.Helpers;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;

namespace AssetManagement.API.DAL.SanityImageDirectory.Repositories;

public class SanityImageRepository : ISanityImageRepository
{
    private readonly IHttpClientFactory _client;
    private readonly string _projectId, _dataset, _token, _apiVersion;

    public SanityImageRepository(IHttpClientFactory http)
    {
        _client = http;

        _projectId = ReturnHelpers.Env("SANITY_PROJECT_ID");
        _dataset = ReturnHelpers.Env("SANITY_PROJECT_DATASET");
        _token = ReturnHelpers.Env("SANITY_PROJECT_API_TOKEN");
        _apiVersion = ReturnHelpers.Env("SANITY_PROJECT_VERSION");

        if (string.IsNullOrEmpty(_projectId) ||
            string.IsNullOrEmpty(_dataset) ||
            string.IsNullOrEmpty(_token))
        {
            throw new InvalidOperationException("Sanity configuration is missing");
        }
    }

    public async Task<SanityUploadResponse?> UploadImageAsync(SanityUploadRequest request)
    {
        var file = request.File;

        if (file == null || file.Length == 0)
            throw new ArgumentException("File not selected");

        var uploadUrl =
            $"https://{_projectId}.api.sanity.io/v{_apiVersion}/assets/images/{_dataset}";

        var client = _client.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _token);

        await using var fileStream = file.OpenReadStream();
        using var content = new StreamContent(fileStream);
        content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        var response = await client.PostAsync(uploadUrl, content);
        if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<SanityUploadResponse>();
        var error = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Sanity upload failed: {error}");
    }

    // ✅ Update document reference to point to new asset
    public async Task<bool> UpdateImageReferenceAsync(string documentId, string newAssetId)
    {
        var client = _client.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _token);

        var url = $"https://{_projectId}.api.sanity.io/v{_apiVersion}/data/mutate/{_dataset}";

        var payload = new
        {
            mutations = new[]
            {
                new {
                    patch = new {
                        id = documentId,
                        set = new {
                            image = new {
                                _type = "image",
                                asset = new { _ref = newAssetId }
                            }
                        }
                    }
                }
            }
        };

        var response = await client.PostAsJsonAsync(url, payload);
        return response.IsSuccessStatusCode;
    }

    // ✅ Count how many docs reference an asset
    public async Task<int> GetAssetReferenceCountAsync(string assetId)
    {
        var client = _client.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _token);

        var query = $"*[_type != 'sanity.imageAsset' && image.asset._ref == '{assetId}']";
        var url = $"https://{_projectId}.api.sanity.io/v{_apiVersion}/data/query/{_dataset}?query={Uri.EscapeDataString(query)}";

        var res = await client.GetFromJsonAsync<SanityQueryResponse>(url);
        return res?.Result?.Count ?? 0;
    }

    // ✅ Delete asset from Sanity
    public async Task<bool> DeleteAssetAsync(string assetId)
    {
        var client = _client.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _token);

        var url = $"https://{_projectId}.api.sanity.io/v{_apiVersion}/data/mutate/{_dataset}";
        var payload = new { mutations = new[] { new { delete = assetId } } };

        var response = await client.PostAsJsonAsync(url, payload);
        return response.IsSuccessStatusCode;
    }
    
    // public async Task CleanupAsync(CancellationToken cancellationToken)
    // {
    //     // Example: find all assets with zero references and delete them
    //     var unusedAssets = await FindUnusedAssetsAsync(); // your own query logic
    //     foreach (var assetId in unusedAssets)
    //     {
    //         if (cancellationToken.IsCancellationRequested) break;
    //         await DeleteAssetAsync(assetId);
    //     }
    // }

}
