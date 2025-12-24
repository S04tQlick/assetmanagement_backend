using System.Linq.Expressions;
using AssetManagement.API.DAL.DatabaseContext; 
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.UserRolesRepository;

public class UserRoleRepository(ApplicationDbContext context)
    : RepositoryQueryHandler<UserRolesModel>(context), IUserRoleRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<UserRolesModel?> GetByUserAndRoleAsync(Guid userId, Guid roleId)
    {
        return await _context.UserRolesModel
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
    }

    public async Task<IEnumerable<UserRolesModel>> GetByUserIdAsync(Guid userId)
    {
        return await _context.UserRolesModel
            .Where(ur => ur.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserRolesModel>> GetByRoleIdAsync(Guid roleId)
    {
        return await _context.UserRolesModel
            .Where(ur => ur.RoleId == roleId)
            .ToListAsync();
    }
}


//, params Expression<Func<UserRolesModel, object>>[] includes