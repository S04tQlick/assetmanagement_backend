using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.Entities.DTOs.Mappings;

public class AssetCategoryMappingProfile : Profile
{
    public AssetCategoryMappingProfile()
    {
        CreateMap<AssetCategoriesCreateRequest, AssetCategoriesModel>();
        CreateMap<AssetCategoriesUpdateRequest, AssetCategoriesModel>();
        CreateMap<AssetCategoriesModel, AssetCategoriesResponse>();
    }
}