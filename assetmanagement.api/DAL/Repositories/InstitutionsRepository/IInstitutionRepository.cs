using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.InstitutionsRepository;

public interface IInstitutionRepository : IRepositoryQueryHandler<InstitutionsModel>
{
    Task<InstitutionsModel?> GetByEmailAsync(string email);
    
    Task<IEnumerable<InstitutionsModel>> GetByDateAsync(DateTime date);
    
    Task<IEnumerable<InstitutionsModel>> GetActiveInstitutionsAsync();
}