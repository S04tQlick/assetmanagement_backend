using Amazon.S3.Model;
using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Services.FileUploadService;

public interface IFileUploadService : IServiceQueryHandler<FileUploadsModel, FileUploadsResponse,
    FileUploadsCreateRequest, FileUploadsUpdateRequest>
{
    new Task<GetObjectResponse> GetByIdAsync(Guid key);
}


// {
//     Task<IEnumerable<FileUploadsResponse>> GetActiveFileUploadsAsync(); 
//     Task<FileUploadsResponse> UploadAndSaveAsync(IFormFile file);
// }