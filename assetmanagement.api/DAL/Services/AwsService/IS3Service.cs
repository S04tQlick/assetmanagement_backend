using Amazon.S3.Model;  

namespace AssetManagement.API.DAL.Services.AwsService;

public interface IS3Service
{
    Task<ListObjectsV2Response> GetAllAsync();
    Task<string> CreateAsync(IFormFile file, bool isLogo);
    Task<GetObjectResponse> GetByIdAsync(string key, bool isLogo);
    Task<string> UpdateAsync(string key, IFormFile file, bool isLogo);
    Task DeleteAsync(string key, bool isLogo);
}
