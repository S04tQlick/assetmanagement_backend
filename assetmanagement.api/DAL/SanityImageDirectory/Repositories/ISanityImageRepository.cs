using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;

namespace AssetManagement.API.DAL.SanityImageDirectory.Repositories;

public interface ISanityImageRepository
{
    Task<SanityUploadResponse?> UploadImageAsync(SanityUploadRequest request);
    Task<bool> UpdateImageReferenceAsync(string documentId, string newAssetId);
    Task<int> GetAssetReferenceCountAsync(string assetId);
    Task<bool> DeleteAssetAsync(string assetId);
    
    //Task CleanupAsync(CancellationToken cancellationToken);
}