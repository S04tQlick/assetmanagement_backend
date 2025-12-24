using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.Entities.DTOs.Mappings;

public class SubscriptionMappingProfile : Profile
{
    public SubscriptionMappingProfile()
    {
        CreateMap<SubscriptionsCreateRequest, SubscriptionsModel>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());

        CreateMap<SubscriptionsUpdateRequest, SubscriptionsModel>();

        CreateMap<SubscriptionsModel, SubscriptionsResponse>()
            .ForMember(
                dest => dest.InstitutionName,
                opt => opt.MapFrom(src => src.Institution!.InstitutionName)
            );
    }
}