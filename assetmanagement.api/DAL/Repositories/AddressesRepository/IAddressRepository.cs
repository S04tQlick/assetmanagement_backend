using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.AddressesRepository;

public interface IAddressRepository : IRepositoryQueryHandler<AddressesModel>
{
    Task<IEnumerable<AddressesModel>> GetByCityAsync(string city);
    Task<IEnumerable<AddressesModel>> GetByCountryAsync(string county);
    Task<IEnumerable<AddressesModel>> GetByRegionAsync(string region);
}