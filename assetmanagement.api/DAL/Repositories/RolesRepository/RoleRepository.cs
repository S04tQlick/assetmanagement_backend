using AssetManagement.API.DAL.DatabaseContext; 
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.RolesRepository;

public class RoleRepository(ApplicationDbContext context) : RepositoryQueryHandler<RolesModel>(context), IRoleRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<RolesModel?> GetByNameAsync(string roleName, CancellationToken cancellationToken = default) =>
        await _context.RolesModel
            .FirstOrDefaultAsync(r => r.RoleName.ToLower() == roleName.ToLower(), cancellationToken);
}