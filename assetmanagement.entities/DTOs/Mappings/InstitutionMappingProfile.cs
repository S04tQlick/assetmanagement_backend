using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.Entities.DTOs.Mappings;

public class InstitutionMappingProfile : Profile
{
    public InstitutionMappingProfile()
    {
        CreateMap<InstitutionsCreateRequest, InstitutionsModel>();
        CreateMap<InstitutionsUpdateRequest, InstitutionsModel>();
        CreateMap<InstitutionsModel, InstitutionsResponse>();
    }
    
    
    // public InstitutionProfile()
    // {
    //     // Request → Entity
    //     CreateMap<InstitutionCreateRequest, Institution>()
    //         .ForMember(dest => dest.Id, opt => opt.Ignore())
    //         .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
    //
    //     CreateMap<InstitutionUpdateRequest, Institution>()
    //         .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
    //
    //     // Entity → Response
    //     CreateMap<Institution, InstitutionResponse>()
    //         .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
    //         .ForMember(dest => dest.SubscriptionEnd, opt => opt.MapFrom(src => src.SubscriptionEnd))
    //         .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.InstitutionUsers));
    // }


}