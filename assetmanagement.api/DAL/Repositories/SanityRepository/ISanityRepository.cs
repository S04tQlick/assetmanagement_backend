using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;

namespace AssetManagement.API.DAL.Repositories.SanityRepository;

public interface ISanityRepository
{
    Task<SanityUploadResponse?> CreateSanityUploadAsync(SanityUploadRequest request);
    Task<SanityUploadResponse?> UpdateSanityUploadAsync(string imageId, SanityUploadRequest request);
}