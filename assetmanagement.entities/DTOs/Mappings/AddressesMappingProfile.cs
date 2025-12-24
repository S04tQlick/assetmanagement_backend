using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.Entities.DTOs.Mappings;

public class AddressesMappingProfile : Profile
{
    public AddressesMappingProfile()
    {
        CreateMap<AddressesCreateRequest, AddressesModel>();
        CreateMap<AddressesUpdateRequest, AddressesModel>();
        CreateMap<AddressesModel, AddressesResponse>();
    }
}