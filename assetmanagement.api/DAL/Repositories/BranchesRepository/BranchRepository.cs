using AssetManagement.API.DAL.DatabaseContext;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.BranchesRepository;

public class BranchRepository(ApplicationDbContext context)
    : RepositoryQueryHandler<BranchesModel>(context), IBranchRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<BranchesModel?> GetByNameAsync(string name, Guid institutionId) =>
        await _context.BranchesModel
            .FirstOrDefaultAsync(b => b.BranchName.Equals(name, StringComparison.CurrentCultureIgnoreCase)
                                      && b.InstitutionId == institutionId);

    public async Task<IEnumerable<BranchesModel>> GetByInstitutionIdAsync(Guid institutionId) =>
        await _context.BranchesModel
            .Where(b => b.InstitutionId == institutionId)
            .ToListAsync();

    public async Task<bool> HasHeadOfficeAsync(Guid institutionId)
    {
        return await _context.BranchesModel
            .AnyAsync(b => b.InstitutionId == institutionId && b.IsHeadOffice);
    }
}