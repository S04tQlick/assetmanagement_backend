using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;

namespace AssetManagement.API.DAL.SanityImageDirectory.Services;

public interface  ISanityImageService
{
    Task<SanityUploadResponse?> CreateAsync(SanityUploadRequest request);
    Task<SanityUploadResponse?> ReplaceImageAsync(string documentId, string oldAssetId, SanityUploadRequest request); 
}