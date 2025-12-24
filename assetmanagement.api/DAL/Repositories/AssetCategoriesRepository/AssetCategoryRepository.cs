using AssetManagement.API.DAL.DatabaseContext;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.AssetCategoriesRepository;

public class AssetCategoryRepository(ApplicationDbContext context)
    : RepositoryQueryHandler<AssetCategoriesModel>(context), IAssetCategoryRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<AssetCategoriesModel?> GetByNameAsync(string categoryName, Guid institutionId,
        CancellationToken cancellationToken = default) =>
        await _context.AssetCategoriesModel
            .FirstOrDefaultAsync(ac => ac.AssetCategoryName.ToLower() == categoryName.ToLower()
                                       && ac.InstitutionId == institutionId, cancellationToken);

    public async Task<IEnumerable<AssetCategoriesModel>> GetByInstitutionIdAndAssetTypeIdAsync(Guid institutionId, Guid typeId,
        CancellationToken cancellationToken = default) =>
        await _context.AssetCategoriesModel
            .Where(ac => ac.InstitutionId == institutionId && ac.AssetTypeId == typeId)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<AssetCategoriesModel>> GetByTypeIdAsync(Guid typeId,
        CancellationToken cancellationToken = default) =>
        await _context.AssetCategoriesModel
            .Where(ac => ac.AssetTypeId == typeId)
            .ToListAsync(cancellationToken);
}