using Amazon.S3;
using Amazon.S3.Model;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.AwsRepository;

public class S3Repository(IAmazonS3 s3, AwsSettingsModel settings) : IS3Repository
{
    private readonly string _bucket = settings.BucketName;
    private readonly string _directory = settings.BucketDirectory;

    public async Task<string> UploadAsync(IFormFile file)
    {
        var fileKey = $"{Guid.NewGuid()}_{file.FileName}"; 
        
        await using var stream = file.OpenReadStream(); 

        var request = new PutObjectRequest
        {
            BucketName = _bucket,
            Key = $"{_directory}/{fileKey}",
            InputStream = stream,
            ContentType = file.ContentType
        };

        await s3.PutObjectAsync(request);
        
        return fileKey;
    }

    public async Task<ListObjectsV2Response> ListAsync()
    {
        return await s3.ListObjectsV2Async(new ListObjectsV2Request
        {
            BucketName = _bucket
        });
    }

    public async Task<GetObjectResponse> GetByIdAsync(string key) =>
        await s3.GetObjectAsync(_bucket, $"{_directory}/{key}");
    
    public async Task<string> UpdateAsync(string key, IFormFile file)
    {
        var fileKey = $"{Guid.NewGuid()}_{file.FileName}"; 
        
        await using var stream = file.OpenReadStream();

        await DeleteAsync(key);
        
        var request = new PutObjectRequest
        {
            BucketName = _bucket,
            Key = $"{_directory}/{fileKey}",
            InputStream = stream,
            ContentType = file.ContentType
        };
        
        await s3.PutObjectAsync(request);
        
        return fileKey;
    }

    public async Task DeleteAsync(string key) =>
        await s3.DeleteObjectAsync(_bucket, $"{_directory}/{key}");
}











// public class S3Repository(IAmazonS3 s3, AwsSettingsModel settingsModel) : IS3Repository
// {
//     private readonly string _bucket = settingsModel.BucketName;
//     private readonly string _directory = settingsModel.BucketDirectory;
//
//     //public async Task<FileUploadsCreateRequest> UploadAsync(IFormFile file)
//     public async Task UploadAsync(IFormFile file)
//     {
//         await using var stream = file.OpenReadStream();
//         var key = $"{_directory}/{Guid.NewGuid()}_{file.FileName}";
//         var url = $"https://{settingsModel.BucketName}.s3.{settingsModel.Region}.amazonaws.com/{key}";
//
//         var request = new PutObjectRequest
//         {
//             BucketName = _bucket,
//             Key = key,
//             InputStream = stream,
//             ContentType = file.ContentType,
//             CannedACL = S3CannedACL.PublicRead
//         };
//
//         await s3.PutObjectAsync(request);
//         
//         // var c=  new FileUploadsCreateRequest()
//         // {
//         //     Id = Guid.NewGuid(),
//         //     S3Key = key,
//         //     CreatedAt = DateTime.UtcNow,
//         //     UpdatedAt = DateTime.UtcNow,
//         //     IsActive = true
//         // };
//         //
//         // fileupload.CreateAsync(request); 
//     }
//
//
//     public async Task<GetObjectResponse> GetAsync(string key)
//     {
//         return await s3.GetObjectAsync(_bucket, $"{_directory}/{key}");
//     }
//
//     public async Task DeleteAsync(string key)
//     {
//         await s3.DeleteObjectAsync(_bucket, key);
//     }
//
//     public async Task<ListObjectsV2Response> ListAsync()
//     {
//         return await s3.ListObjectsV2Async(new ListObjectsV2Request
//         {
//             BucketName = _bucket
//         });
//     }
// }