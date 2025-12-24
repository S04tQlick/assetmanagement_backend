using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler; 
using AssetManagement.Entities.Models;
using AutoMapper;


namespace AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;


public abstract class ServiceQueryHandler<TEntity, TResponse, TCreateRequest, TUpdateRequest>(IRepositoryQueryHandler<TEntity> repository, IMapper mapper) : IServiceQueryHandler<TEntity, TResponse, TCreateRequest, TUpdateRequest> where TEntity : BaseModel
{
    public virtual async Task<IEnumerable<TResponse>> GetAllAsync()
    {
        var entities = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<TResponse>>(entities);
    }

    public virtual async Task<TResponse?> GetByIdAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);
        return entity == null ? default : mapper.Map<TResponse>(entity);
    }

    public virtual async Task<int> CreateAsync(TCreateRequest request)
    {
        var entity = mapper.Map<TEntity>(request);
        
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;

        var created = await repository.CreateAsync(entity);
        return mapper.Map<int>(created);
    }

    public virtual async Task<int> UpdateAsync(Guid id, TUpdateRequest request)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return 0;

        mapper.Map(request, entity);
        entity.UpdatedAt = DateTime.UtcNow;

        var updated = await repository.UpdateAsync(entity);
        return mapper.Map<int>(updated);
    }

    public virtual async Task<int> DeleteAsync(Guid id) => await repository.DeleteAsync(id);
}
