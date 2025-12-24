using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.Entities.DTOs.Mappings;

public class BranchMappingProfile : Profile
{
    public BranchMappingProfile()
    {
        CreateMap<BranchesCreateRequest, BranchesModel>();
        CreateMap<BranchesUpdateRequest, BranchesModel>();
        CreateMap<BranchesModel, BranchesResponse>();
    }
}