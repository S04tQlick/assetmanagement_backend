using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.Entities.DTOs.Mappings;

public class UsersMappingProfile : Profile
{
    public UsersMappingProfile()
    {
        CreateMap<UsersCreateRequest, UsersModel>()
            .ForMember(
                dest => dest.PasswordHash,
                opt => opt.Ignore()
            );

        CreateMap<UsersUpdateRequest, UsersModel>()
            .ForMember(
                dest => dest.PasswordHash,
                opt => opt.Ignore()
            ).AfterMap((src, dest) => { dest.DisplayName = $"{src.FirstName} {src.LastName}"; }).ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string s && string.IsNullOrWhiteSpace(s))));

        CreateMap<UsersModel, UsersResponse>();
    }
}