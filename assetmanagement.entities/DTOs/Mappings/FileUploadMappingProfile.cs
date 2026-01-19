using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.Entities.DTOs.Mappings;

public class FileUploadMappingProfile : Profile
{
    public FileUploadMappingProfile()
    {
        CreateMap<FileUploadsCreateRequest, FileUploadsModel>();
        CreateMap<FileUploadsUpdateRequest, FileUploadsModel>();
        CreateMap<FileUploadsModel, FileUploadsResponse>();
    }
}