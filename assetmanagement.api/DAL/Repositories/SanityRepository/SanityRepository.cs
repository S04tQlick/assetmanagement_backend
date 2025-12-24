using System.Net.Http.Headers;
using AssetManagement.API.Helpers;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;

namespace AssetManagement.API.DAL.Repositories.SanityRepository;

public class SanityRepository(IHttpClientFactory clientFactory) : ISanityRepository
{
    public async Task<SanityUploadResponse?> CreateSanityUploadAsync(SanityUploadRequest request)
    {
        var file = request.File;

        if (file == null || file.Length == 0)
            throw new ArgumentException("File not selected");

        var sanityProjectId = ReturnHelpers.Env("SANITY_PROJECT_ID");
        var sanityDataset = ReturnHelpers.Env("SANITY_PROJECT_DATASET");
        var sanityToken = ReturnHelpers.Env("SANITY_PROJECT_API_TOKEN");
        var sanityProjectVersion = ReturnHelpers.Env("SANITY_PROJECT_VERSION");

        if (string.IsNullOrEmpty(sanityProjectId) ||
            string.IsNullOrEmpty(sanityDataset) ||
            string.IsNullOrEmpty(sanityToken))
        {
            throw new InvalidOperationException("Sanity configuration is missing");
        }

        var uploadUrl =
            $"https://{sanityProjectId}.api.sanity.io/v{sanityProjectVersion}/assets/images/{sanityDataset}";

        var client = clientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", sanityToken);

        await using var fileStream = file.OpenReadStream();
        using var content = new StreamContent(fileStream);
        content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        var response = await client.PostAsync(uploadUrl, content);
        if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<SanityUploadResponse>();
        var error = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Sanity upload failed: {error}");

    }

    public async Task<SanityUploadResponse?> UpdateSanityUploadAsync(string imageId, SanityUploadRequest request)
    {
        var file = request.File;

        if (file == null || file.Length == 0)
            throw new ArgumentException("File not selected");

        var sanityProjectId = ReturnHelpers.Env("SANITY_PROJECT_ID");
        var sanityDataset = ReturnHelpers.Env("SANITY_PROJECT_DATASET");
        var sanityToken = ReturnHelpers.Env("SANITY_PROJECT_API_TOKEN");
        var sanityProjectVersion = ReturnHelpers.Env("SANITY_PROJECT_VERSION");

        if (string.IsNullOrEmpty(sanityProjectId) ||
            string.IsNullOrEmpty(sanityDataset) ||
            string.IsNullOrEmpty(sanityToken))
        {
            throw new InvalidOperationException("Sanity configuration is missing");
        }

        var uploadUrl =
            $"https://{sanityProjectId}.api.sanity.io/v{sanityProjectVersion}/assets/images/{sanityDataset}";

        var client = clientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", sanityToken);

        await using var fileStream = file.OpenReadStream();
        using var content = new StreamContent(fileStream);
        content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        var response = await client.PostAsync(uploadUrl, content);
        if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<SanityUploadResponse>();
        var error = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Sanity upload failed: {error}");
    }


    public async Task<SanityUploadResponse?> UpdateSanityUploadAsync(
        string documentId, // ⬅ ID of sanity document to update
        string oldAssetId, // ⬅ Old asset id: image-xxxxx-2000x3000-jpg
        SanityUploadRequest request
    )
    {
        if (request.File is null || request.File.Length == 0)
            throw new ArgumentException("No file selected for upload");

        var sanityProjectId = ReturnHelpers.Env("SANITY_PROJECT_ID")!;
        var sanityDataset = ReturnHelpers.Env("SANITY_PROJECT_DATASET")!;
        var sanityToken = ReturnHelpers.Env("SANITY_PROJECT_API_TOKEN")!;
        var sanityVersion = ReturnHelpers.Env("SANITY_PROJECT_VERSION")!;

        var client = clientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sanityToken);

        // 1️⃣ Upload the new image asset
        var uploadUrl = $"https://{sanityProjectId}.api.sanity.io/v{sanityVersion}/assets/images/{sanityDataset}";

        await using var stream = request.File.OpenReadStream();
        using var content = new StreamContent(stream);
        content.Headers.ContentType = new MediaTypeHeaderValue(request.File.ContentType);

        var uploadResponse = await client.PostAsync(uploadUrl, content);
        if (!uploadResponse.IsSuccessStatusCode)
            throw new Exception("Upload failed: " + await uploadResponse.Content.ReadAsStringAsync());

        var newAsset = await uploadResponse.Content.ReadFromJsonAsync<SanityUploadResponse>();

        if (newAsset == null || string.IsNullOrEmpty(newAsset.Document.Id))
            throw new Exception("Upload returned empty response");

        var newAssetId = newAsset.Document.Id; // New Sanity asset id

        // 2️⃣ Update sanity document to reference the new image
        var patchPayload = new
        {
            patch = new
            {
                id = documentId,
                set = new
                {
                    image = new
                    {
                        _type = "image",
                        asset = new { _ref = newAssetId }
                    }
                }
            }
        };

        var patchUrl = $"https://{sanityProjectId}.api.sanity.io/v{sanityVersion}/data/mutate/{sanityDataset}";

        var patchResponse = await client.PostAsJsonAsync(patchUrl, patchPayload);
        if (!patchResponse.IsSuccessStatusCode)
            throw new Exception("Update reference failed: " + await patchResponse.Content.ReadAsStringAsync());


        // 3️⃣ Delete old asset from Sanity
        var deleteUrl = $"https://{sanityProjectId}.api.sanity.io/v{sanityVersion}/assets/images/{oldAssetId}";
        var deleteResponse = await client.DeleteAsync(deleteUrl);

        return !deleteResponse.IsSuccessStatusCode ? throw new Exception("Old asset deletion failed: " + await deleteResponse.Content.ReadAsStringAsync()) : newAsset; // ⬅ return new uploaded asset info
    }
}