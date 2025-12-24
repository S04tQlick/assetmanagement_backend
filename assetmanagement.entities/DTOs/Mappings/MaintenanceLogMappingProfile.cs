using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.Entities.DTOs.Mappings;

public class MaintenanceLogsMappingProfile : Profile
{
    public MaintenanceLogsMappingProfile()
    {
        CreateMap<MaintenanceLogsModel, MaintenanceLogsResponse>()
            .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.Asset!.AssetName))
            .ForMember(dest => dest.InstitutionName, opt => opt.MapFrom(src => src.Institution!.InstitutionName));

        CreateMap<MaintenanceLogsRequest.MaintenanceLogsCreateRequest, MaintenanceLogsModel>();
        CreateMap<MaintenanceLogsRequest.MaintenanceLogUpdateRequest, MaintenanceLogsModel>();
    }
}