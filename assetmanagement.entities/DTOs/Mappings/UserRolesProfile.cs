using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.Entities.DTOs.Mappings;

public class UserRolesProfile : Profile
{
    public UserRolesProfile()
    {
        CreateMap<UserRolesCreateRequest, UserRolesModel>();
        CreateMap<UserRolesUpdateRequest, UserRolesModel>();
        CreateMap<UserRolesModel, UserRolesResponse>();
    }
}