using AssetManagement.API.DAL.DatabaseContext;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.AddressesRepository;

public class AddressRepository (ApplicationDbContext context) : RepositoryQueryHandler<AddressesModel>(context), IAddressRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<AddressesModel>> GetByCityAsync(string city)
    {
        return await _context.AddressesModel
            .Where(a => a.City == city)
            .ToListAsync();
    }

    public async Task<IEnumerable<AddressesModel>> GetByCountryAsync(string county)
    {
        return await _context.AddressesModel
            .Where(a => a.City == county)
            .ToListAsync();
    }

    public async Task<IEnumerable<AddressesModel>> GetByRegionAsync(string region)
    {
        return await _context.AddressesModel
            .Where(a => a.City == region)
            .ToListAsync();
    }
}