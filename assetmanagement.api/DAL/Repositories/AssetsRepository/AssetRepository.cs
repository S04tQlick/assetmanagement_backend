using AssetManagement.API.DAL.DatabaseContext;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Enums;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.AssetsRepository;

public class AssetRepository(ApplicationDbContext context)
    : RepositoryQueryHandler<AssetsModel>(context), IAssetRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<AssetsModel>> GetAllAsync() =>
        await _context.AssetsModel
            .Include(a => a.Institutions)
            .Include(a => a.AssetCategories)
            .Include(a => a.Branches)
            .ToListAsync();

    public async Task<IEnumerable<AssetsModel>> GetFilteredAsync(
        Guid? institutionId,
        Guid? branchId,
        Guid? assetTypeId,
        Guid? assetCategoryId,
        Guid? vendorId,
        DepreciationMethodEnum? method,
        DateTime? fromDate,
        DateTime? toDate)
    {
        var query = _context.AssetsModel
            .Include(a => a.Institutions)
            .Include(a => a.Branches)
            .Include(a => a.AssetCategories)
            .AsQueryable();

        if (institutionId.HasValue)
            query = query.Where(a => a.InstitutionId == institutionId.Value);

        if (branchId.HasValue)
            query = query.Where(a => a.BranchId == branchId.Value);

        if (assetTypeId.HasValue)
            query = query.Where(a => a.AssetTypeId == assetTypeId.Value);

        if (assetCategoryId.HasValue)
            query = query.Where(a => a.AssetCategoryId == assetCategoryId.Value);

        if (vendorId.HasValue)
            query = query.Where(a => a.VendorId == vendorId.Value);

        if (method.HasValue)
            query = query.Where(a => a.DepreciationMethod == method.Value.ToString());

        if (fromDate.HasValue)
            query = query.Where(a => a.PurchaseDate >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(a => a.PurchaseDate <= toDate.Value);

        return await query.ToListAsync();
    } 
    
    public async Task<IEnumerable<AssetsModel>> GetMaintenanceDueAsync(Guid institutionId, Guid branchId) =>
        await _context.AssetsModel
            .Where(asset => asset.NextMaintenanceDate <= DateTime.UtcNow.Date && asset.InstitutionId == institutionId &&
                            asset.BranchId == branchId)
            .ToListAsync();

    public async Task<IEnumerable<AssetsModel>> GetByInstitutionIdAsync(Guid institutionId) =>
        await _context.AssetsModel
            .Where(b => b.InstitutionId == institutionId)
            .ToListAsync();

    public async Task<AssetsModel?> GetByIdAsync(Guid id) =>
        await _context.AssetsModel
            .Include(a => a.Institutions)
            .Include(a => a.AssetCategories)
            .Include(a => a.Branches)
            .FirstOrDefaultAsync(a => a.Id == id);

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}