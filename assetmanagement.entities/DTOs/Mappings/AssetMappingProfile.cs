using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.Entities.DTOs.Mappings;

public class AssetMappingProfile : Profile
{
    public AssetMappingProfile()
    {
        CreateMap<AssetsModel, AssetsResponse>();
        CreateMap<AssetsCreateRequest, AssetsModel>();
        CreateMap<AssetsUpdateRequest, AssetsModel>();
    }
}