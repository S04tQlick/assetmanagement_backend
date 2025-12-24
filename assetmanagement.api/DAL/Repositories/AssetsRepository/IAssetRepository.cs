using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Enums;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.AssetsRepository;

public interface IAssetRepository : IRepositoryQueryHandler<AssetsModel>
{
    Task<IEnumerable<AssetsModel>> GetByInstitutionIdAsync(Guid institutionId);

    Task<IEnumerable<AssetsModel>> GetFilteredAsync(
        Guid? institutionId,
        Guid? branchId,
        Guid? assetTypeId,
        Guid? assetCategoryId,
        Guid? vendorId,
        DepreciationMethodEnum? method,
        DateTime? fromDate,
        DateTime? toDate);
    
    Task<IEnumerable<AssetsModel>> GetMaintenanceDueAsync(Guid institutionId, Guid branchId); 
    Task SaveChangesAsync();
}