using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;

public interface IServiceQueryHandler<TEntity, TResponse, in TCreateRequest, in TUpdateRequest> where TEntity : BaseModel
{
    HealthResponse GetHealth();
    
    Task<IEnumerable<TResponse>> GetAllAsync();

    Task<TResponse?> GetByIdAsync(Guid id);
    
    Task<IEnumerable<TResponse>> GetByInstitutionIdAsync(Guid institutionId);
    
    Task<IEnumerable<TResponse>> IsActiveAsync();
    
    Task<IEnumerable<TResponse>> IsNotActiveAsync();

    Task<TEntity> CreateAsync(TCreateRequest request);

    Task <TEntity> UpdateAsync(Guid id, TUpdateRequest request);

    Task<int> DeleteAsync(Guid id);
}