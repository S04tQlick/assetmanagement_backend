using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.Entities.DTOs.Mappings;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<RolesCreateRequest, RolesModel>();
        CreateMap<RolesUpdateRequest, RolesModel>();
        CreateMap<RolesModel, RolesResponse>();
    }
}