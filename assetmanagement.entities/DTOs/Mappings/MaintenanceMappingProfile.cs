using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.Entities.DTOs.Mappings;

public class MaintenanceMappingProfile : Profile
{
    public MaintenanceMappingProfile()
    {
        CreateMap<MaintenancesCreateRequest, MaintenancesModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<MaintenancesUpdateRequest, MaintenancesModel>();

        CreateMap<MaintenancesModel, MaintenancesResponse>()
            .ForMember(
                dest => dest.AssetName,
                opt => opt.MapFrom(src => src.Asset!.AssetName)
            )
            
            .ForMember(
                dest => dest.VendorName,
                opt => opt.MapFrom(src => src.Vendor!.VendorsName)
            );
    }
}