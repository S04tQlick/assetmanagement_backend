using Amazon.S3.Model;

namespace AssetManagement.API.DAL.Repositories.AwsRepository;

public interface IS3Repository
{
    Task<string> UploadAsync(IFormFile file, bool isLogo);
    Task<GetObjectResponse> GetByIdAsync(string key, bool isLogo);
    Task DeleteAsync(string key, bool isLogo);
    Task<ListObjectsV2Response> ListAsync();
    Task<string> UpdateAsync(string key, IFormFile file, bool isLogo);
}