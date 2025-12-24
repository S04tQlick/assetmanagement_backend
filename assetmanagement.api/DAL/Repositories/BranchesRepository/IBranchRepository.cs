using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.BranchesRepository;

public interface IBranchRepository : IRepositoryQueryHandler<BranchesModel>
{
    Task<BranchesModel?> GetByNameAsync(string name, Guid institutionId);
    Task<IEnumerable<BranchesModel>> GetByInstitutionIdAsync(Guid institutionId);
    Task<bool> HasHeadOfficeAsync(Guid institutionId);
}