using AssetManagement.API.DAL.DatabaseContext;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.API.DAL.Repositories.VendorsRepository;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.MaintenanceLogsRepository;

public class MaintenanceLogRepository(ApplicationDbContext context)
    : RepositoryQueryHandler<VendorsModel>(context), IVendorRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<VendorsModel?> GetByNameAsync(string name, Guid institutionId) =>
        await _context.VendorsModel
            .FirstOrDefaultAsync(b => b.VendorsName.ToLower() == name.ToLower()
                                      && b.InstitutionId == institutionId);

    public async Task<IEnumerable<VendorsModel>> GetByInstitutionIdAsync(Guid institutionId) =>
        await _context.VendorsModel
            .Where(b => b.InstitutionId == institutionId)
            .ToListAsync();
}