using Amazon.S3.Model;
using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.API.DAL.Repositories.FileUploadsRepository;
using AssetManagement.API.DAL.Services.AwsService;
using AssetManagement.API.Exceptions;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.API.DAL.Services.FileUploadService;

public class FileUploadService(IFileUploadRepository repository, IS3Service s3, IMapper mapper) : ServiceQueryHandler<FileUploadsModel, FileUploadsResponse, FileUploadsCreateRequest, FileUploadsUpdateRequest>(repository, mapper), IFileUploadService
{
    public new async Task<FileUploadsModel> CreateAsync(FileUploadsCreateRequest request)
    {
        var key = await s3.CreateAsync(request.File, request.IsLogo);
         
        request.S3Key = key;
        request.IsActive = true;
        request.IsLogo = request.IsLogo;

        return await base.CreateAsync(request);
    }

    public new async Task<GetObjectResponse> GetByIdAsync(Guid id)
    {
        var existing = await repository.GetByIdAsync(id); 
        if (existing is null) 
            throw new NotFoundException($"File with id '{id}' not found.");
        
        return await s3.GetByIdAsync(existing.S3Key, existing.IsLogo);
    }

    public new async Task<FileUploadsModel> UpdateAsync(Guid id, FileUploadsUpdateRequest request)
    {
        var existing = await repository.GetByIdAsync(id);
        if (existing is null)
            throw new NotFoundException($"File with id '{id}' not found.");

        request.IsActive = true;
        request.IsLogo = request.IsLogo;
        request.S3Key = await s3.UpdateAsync(existing.S3Key, request.File, request.IsLogo);
        return await base.UpdateAsync(id, request);
    }

    public new async Task<int> DeleteAsync(Guid id)
    {
        var existing = await repository.GetByIdAsync(id);
        if (existing is null)
            throw new NotFoundException($"File with id '{id}' not found.");

        await s3.DeleteAsync(existing.S3Key, existing.IsLogo);
        return await base.DeleteAsync(id);
    }
}