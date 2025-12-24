using System.Linq.Expressions;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;

public interface IServiceQueryHandler<TEntity, TResponse, in TCreateRequest, in TUpdateRequest> where TEntity : BaseModel
{
    Task<IEnumerable<TResponse>> GetAllAsync();

    Task<TResponse?> GetByIdAsync(Guid id);

    Task<int> CreateAsync(TCreateRequest request);

    Task <int> UpdateAsync(Guid id, TUpdateRequest request);

    Task<int> DeleteAsync(Guid id);
}
    
    
    
    // Task<IEnumerable<TResponse>> GetAllAsync
    // (
    //     params Expression<Func<TEntity, object>>[] includes
    // );
    //
    // Task<TResponse?> GetByIdAsync
    // (
    //     Guid id,
    //     params Expression<Func<TEntity, object>>[] includes
    // );
    //
    // Task<TResponse> CreateAsync
    // (
    //     TCreateRequest request
    // );
    //
    // Task<TResponse?> UpdateAsync
    // (
    //     Guid id,
    //     TUpdateRequest request
    // );
    //
    // Task<int> DeleteAsync
    //     (Guid id);
    //
    // Task<IEnumerable<TResponse>> FindAsync
    // (
    //     Expression<Func<TEntity, bool>> predicate,
    //     params Expression<Func<TEntity, object>>[] includes
    // );