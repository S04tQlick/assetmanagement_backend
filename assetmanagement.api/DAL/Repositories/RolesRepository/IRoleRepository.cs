using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.RolesRepository;

public interface IRoleRepository : IRepositoryQueryHandler<RolesModel>
{
    Task<RolesModel?> GetByNameAsync(string roleName, CancellationToken cancellationToken = default);
}