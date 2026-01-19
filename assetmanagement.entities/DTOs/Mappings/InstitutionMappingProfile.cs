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
}