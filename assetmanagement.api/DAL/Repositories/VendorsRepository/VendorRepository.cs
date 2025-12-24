using AssetManagement.API.DAL.DatabaseContext;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.VendorsRepository;

public class VendorRepository(ApplicationDbContext context)
    : RepositoryQueryHandler<VendorsModel>(context), IVendorRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<VendorsModel?> GetByNameAsync(string name, Guid institutionId) =>
        await _context.VendorsModel
            .FirstOrDefaultAsync(b => string.Equals(b.VendorsName, name, StringComparison.OrdinalIgnoreCase)
                                      && b.InstitutionId == institutionId);

    public async Task<IEnumerable<VendorsModel>> GetByInstitutionIdAsync(Guid institutionId) =>
        await _context.VendorsModel
            .Where(b => b.InstitutionId == institutionId)
            .ToListAsync();
}