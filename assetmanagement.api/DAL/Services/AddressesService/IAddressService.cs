using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Services.AddressesService;

public interface IAddressService : IServiceQueryHandler<AddressesModel, AddressesResponse, AddressesCreateRequest, AddressesUpdateRequest>
{ 
    // Task<IEnumerable<AddressesResponse>> GetByCityAsync(string city);
    // Task<IEnumerable<AddressesResponse>> GetByCountryAsync(string county);
    // Task<IEnumerable<AddressesResponse>> GetByRegionAsync(string region);
}