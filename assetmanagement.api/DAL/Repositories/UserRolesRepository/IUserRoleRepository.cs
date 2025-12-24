using System.Linq.Expressions;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.UserRolesRepository;

public interface IUserRoleRepository : IRepositoryQueryHandler<UserRolesModel>
{
    Task<IEnumerable<UserRolesModel>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<UserRolesModel>> GetByRoleIdAsync(Guid roleId);
}


//, params Expression<Func<UserRolesModel, object>>[] includes