using Amazon.S3.Model;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.FileUploadsRepository;

public interface IFileUploadRepository : IRepositoryQueryHandler<FileUploadsModel>
{
    //Task<GetObjectResponse> GetByIdAsync(string key);
}