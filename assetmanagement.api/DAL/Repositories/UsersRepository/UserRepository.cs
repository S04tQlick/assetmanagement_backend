using AssetManagement.API.DAL.DatabaseContext;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.UsersRepository;

public class UserRepository(ApplicationDbContext context) : RepositoryQueryHandler<UsersModel>(context), IUserRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<UsersModel?> GetByEmailAsync(string email) =>
        await _context.UsersModel
            .FirstOrDefaultAsync(u => string.Equals(u.EmailAddress, email, StringComparison.OrdinalIgnoreCase));

    public async Task<UsersModel?> GetUserByIdAndInstitutionIdAsync(Guid institutionId,  Guid userId) =>
        await _context.UsersModel
            .FirstOrDefaultAsync(u => (u.InstitutionId == institutionId)  && (u.Id == userId));

    public async Task<IEnumerable<UsersModel>> GetByInstitutionIdAsync(Guid institutionId) =>
        await _context.UsersModel
            .Where(iu => iu.InstitutionId == institutionId)
            .ToListAsync();
}