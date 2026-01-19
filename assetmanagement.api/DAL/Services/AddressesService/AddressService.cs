using System.Linq.Expressions;
using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.API.DAL.Repositories.AddressesRepository;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.API.DAL.Services.AddressesService;

public class AddressService(IAddressRepository repository, IMapper mapper)
    : ServiceQueryHandler<AddressesModel, AddressesResponse, AddressesCreateRequest, AddressesUpdateRequest>(repository, mapper), IAddressService
{
    protected override Expression<Func<AddressesModel, bool>> IsExistsPredicate(AddressesCreateRequest request)
    {
        return  x =>
            x.Street == request.Street &&
            x.PostalCode == request.PostalCode &&
            x.QueryId == request.QueryId;
    }
    
    protected override Expression<Func<AddressesModel, bool>> UpdateIsExistsPredicate(Guid id, AddressesUpdateRequest request)
    {
        return  x =>
            x.Id != id &&
            x.Street == request.Street &&
            x.PostalCode == request.PostalCode &&
            x.QueryId == request.QueryId;
    }
}


// {
//     private readonly IMapper _mapper = mapper;
//     private readonly IRepositoryQueryHandler<AddressesModel> _repository = repository;
//     
//     protected override Expression<Func<AddressesModel, bool>> CreateExistsPredicate(AddressesCreateRequest request)
//     {
//         return x =>  
//             x.Street == request.Street && 
//             x.PostalCode == request.PostalCode;
//     }
//
//     protected override Expression<Func<AddressesModel, bool>> UpdateExistsPredicate(Guid id, AddressesUpdateRequest request)
//     {
//         return x =>
//             x.Id != id &&
//             x.Street == request.Street &&
//             x.PostalCode == request.PostalCode;
//     }
//
//     public async Task<IEnumerable<AddressesResponse>> GetActiveAddressesAsync()
//     {
//         var activeEntities = await base.GetAllAsync();
//         return _mapper.Map<IEnumerable<AddressesResponse>>(activeEntities);
//     }
//     
//     
//     
//     
//     
//
//     // public async Task<IEnumerable<AddressesResponse>> GetByCityAsync(string city)
//     // {
//     //     var entities = await _repository.GetByCityAsync(city);
//     //     return  _mapper.Map<IEnumerable<AddressesResponse>>(entities);
//     // }
//     //
//     // public async Task<IEnumerable<AddressesResponse>> GetByCountryAsync(string county)
//     // {
//     //     var entities = await _repository.GetByCityAsync(county);
//     //     return  _mapper.Map<IEnumerable<AddressesResponse>>(entities);
//     // }
//     //
//     // public async Task<IEnumerable<AddressesResponse>> GetByRegionAsync(string region)
//     // {
//     //     var entities = await _repository.GetByCityAsync(region);
//     //     return  _mapper.Map<IEnumerable<AddressesResponse>>(entities);
//     // }
// }