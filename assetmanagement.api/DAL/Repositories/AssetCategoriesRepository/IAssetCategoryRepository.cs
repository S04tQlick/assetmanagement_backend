using System.Linq.Expressions;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.AssetCategoriesRepository;

public interface IAssetCategoryRepository : IRepositoryQueryHandler<AssetCategoriesModel>
{
    // Task<bool> ExistsAsync(Expression<Func<AssetTypesModel, bool>> predicate); 
    Task<IEnumerable<AssetCategoriesModel>> GetByInstitutionIdAndAssetTypeIdAsync(Guid institutionId, Guid typeId, CancellationToken cancellationToken = default); 
}