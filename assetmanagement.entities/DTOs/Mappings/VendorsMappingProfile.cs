using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.Entities.DTOs.Mappings;

public class VendorsMappingProfile : Profile
{
    public VendorsMappingProfile()
    {
        CreateMap<VendorsModel, VendorsResponse>();
        CreateMap<VendorsCreateRequest, VendorsModel>();
        CreateMap<VendorsUpdateRequest, VendorsModel>();
    }
}