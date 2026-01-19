using Amazon.S3.Model;  

namespace AssetManagement.API.DAL.Services.AwsService;

public interface IS3Service
{
    Task<ListObjectsV2Response> GetAllAsync();
    Task<string> CreateAsync(IFormFile file);
    Task<GetObjectResponse> GetByIdAsync(string key);
    Task<string> UpdateAsync(string key, IFormFile file);
    Task DeleteAsync(string key);
}
