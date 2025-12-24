using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.Entities.DTOs.Mappings;

public class AssetTypeMappingProfile : Profile
{
    public AssetTypeMappingProfile()
    {
        CreateMap<AssetTypesCreateRequest, AssetTypesModel>();
        CreateMap<AssetTypesUpdateRequest, AssetTypesModel>();
        CreateMap<AssetTypesModel, AssetTypesResponse>();
    }
}