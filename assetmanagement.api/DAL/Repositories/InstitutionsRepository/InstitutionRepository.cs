using AssetManagement.API.DAL.DatabaseContext;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;


namespace AssetManagement.API.DAL.Repositories.InstitutionsRepository;

public class InstitutionRepository(ApplicationDbContext context)
    : RepositoryQueryHandler<InstitutionsModel>(context), IInstitutionRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> ExistsAsync(string name) =>
        await _context.InstitutionsModel
            .AnyAsync(i => i.InstitutionName.Equals(name, StringComparison.CurrentCultureIgnoreCase));

    public async Task<InstitutionsModel?>
        GetByEmailAsync(string email) =>
        await _context.InstitutionsModel
            .FirstOrDefaultAsync(i => i.InstitutionEmail.Equals(email, StringComparison.CurrentCultureIgnoreCase));

    public async Task<IEnumerable<InstitutionsModel>> GetByDateAsync(DateTime date)
    {
        var startUtc = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
        return await _context.InstitutionsModel
            .Where(i => i.CreatedAt >= startUtc && i.CreatedAt < startUtc.AddDays(1))
            .ToListAsync();
    }
    
    public async Task<IEnumerable<InstitutionsModel>> GetActiveInstitutionsAsync()
    {
        return await _context.InstitutionsModel
            .Where(at => at.IsActive)
            .ToListAsync();
    }
}