using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.UsersRepository;

public interface IUserRepository : IRepositoryQueryHandler<UsersModel>
{
    Task<UsersModel?> GetByEmailAsync(string email);
    Task<UsersModel?> GetUserByIdAndInstitutionIdAsync(Guid institutionId, Guid userId);
    Task<IEnumerable<UsersModel>> GetByInstitutionIdAsync(Guid institutionId);
}