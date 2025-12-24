using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.VendorsRepository;

public interface IVendorRepository : IRepositoryQueryHandler<VendorsModel>
{
    Task<VendorsModel?> GetByNameAsync(string name, Guid institutionId);
    Task<IEnumerable<VendorsModel>> GetByInstitutionIdAsync(Guid institutionId);
}
