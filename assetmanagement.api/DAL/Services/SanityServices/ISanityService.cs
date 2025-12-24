using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.GeneralResponse;

namespace AssetManagement.API.DAL.Services.SanityServices;

public interface ISanityService
{
     Task <BaseActionResponse<object>> CreateAsync(SanityUploadRequest request);
} 