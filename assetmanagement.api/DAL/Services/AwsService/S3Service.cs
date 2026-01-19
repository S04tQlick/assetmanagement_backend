using Amazon.S3.Model;
using AssetManagement.API.DAL.Repositories.AwsRepository;
using Serilog;

namespace AssetManagement.API.DAL.Services.AwsService;


public class S3Service(IS3Repository s3Repo) : IS3Service
{
    public async Task<string> CreateAsync(IFormFile file, bool isLogo)
    {
        Log.Information("Starting file upload: {FileName}", file.FileName);
        return await s3Repo.UploadAsync(file, isLogo);
    }

    public async Task<ListObjectsV2Response> GetAllAsync()
    {
        return await s3Repo.ListAsync();
    }

    public async Task<GetObjectResponse> GetByIdAsync(string key, bool isLogo)
    {
        return await s3Repo.GetByIdAsync(key, isLogo);
    }

    public async Task<string> UpdateAsync(string key, IFormFile file, bool isLogo)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is empty");

        return await s3Repo.UpdateAsync(key, file, isLogo);
    }

    public async Task DeleteAsync(string key, bool isLogo)
    {
        await s3Repo.DeleteAsync(key, isLogo);
    }
}














// public class S3Service(IS3Repository repo) : IS3Service
// {
//     public async Task UploadAsync(IFormFile file)
//     {
//         Log.Information("Starting file upload: {FileName}", file.FileName);
//
//         var image = await repo.UploadAsync(file);
//
//         Log.Information("Upload complete. S3 Key: {Key}", image.S3Key);
//
//         // return new FileUploadsResponse
//         // {
//         //     Id = image.Id,
//         //     S3Key = image.S3Key
//         // };
//     }
//     
//     public async Task<GetObjectResponse> GetAsync(string key) =>
//         await repo.GetAsync(key);
//
//     public async Task DeleteAsync(string key)
//     {
//         Log.Information("Deleted S3 object: {Key}", key);
//         await repo.DeleteAsync(key);
//     }
//
//     public async Task<ListObjectsV2Response> ListAsync() =>
//         await repo.ListAsync();
// }
























// public class S3Service(IAmazonS3 s3)
// {
//     private readonly string _bucket = ReturnHelpers.Env("AWS_BUCKET_NAME");
//     private readonly string _directory = ReturnHelpers.Env("AWS_BUCKET_DIRECTORY");
//
//     // CREATE / UPDATE (Upload)
//     public async Task UploadAsync(IFormFile file)
//     {
//         await using var stream = file.OpenReadStream();
//         var key = $"{_directory}/{Guid.NewGuid()}_{file.FileName}"; 
//         
//         var request = new PutObjectRequest
//         {
//             BucketName = _bucket,
//             Key = key,
//             InputStream = stream,
//             ContentType = file.ContentType
//         };
//         
//         await s3.PutObjectAsync(request); 
//     }
//
//     // READ (Download)
//     public async Task<GetObjectResponse> GetAsync(string key)
//     {
//         return await s3.GetObjectAsync(_bucket, key);
//     }
//
//     // DELETE
//     public async Task DeleteAsync(string key)
//     {
//         await s3.DeleteObjectAsync(_bucket, key);
//     }
//
//     // LIST (Optional)
//     public async Task<ListObjectsV2Response> ListAsync()
//     {
//         return await s3.ListObjectsV2Async(new ListObjectsV2Request
//         {
//             BucketName = _bucket
//         });
//     }
// }