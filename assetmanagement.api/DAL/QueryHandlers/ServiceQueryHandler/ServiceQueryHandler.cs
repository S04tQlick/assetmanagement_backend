using System.Linq.Expressions;
using AssetManagement.API.Constants;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.API.Exceptions;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;
using Serilog;

namespace AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;

public abstract class ServiceQueryHandler<TEntity, TResponse, TCreateRequest, TUpdateRequest>(IRepositoryQueryHandler<TEntity> repository, IMapper mapper) : IServiceQueryHandler<TEntity, TResponse, TCreateRequest, TUpdateRequest> where TEntity : BaseModel
{ 
   
    public HealthResponse GetHealth()
    {
        Log.Information("Health action queried successfully.");
        return new HealthResponse
        {
            Message = ControllerConstants.HealthMessage,
            Timestamp = DateTime.UtcNow
        };
    }

    public async Task<IEnumerable<TResponse>> GetAllAsync()
    {
        var entities = await repository.GetAllAsync(DefaultIncludes()); 
        return mapper.Map<IEnumerable<TResponse>>(entities);
    }

    public async Task<TResponse?> GetByIdAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id, DefaultIncludes()); 
        return entity == null ? default : mapper.Map<TResponse>(entity);
    }

    public async Task<IEnumerable<TResponse>> GetByInstitutionIdAsync(Guid institutionId)
    {
        var entities = await repository.GetByInstitutionIdAsync(institutionId);
        return mapper.Map<IEnumerable<TResponse>>(entities);
    }

    public async Task<IEnumerable<TResponse>> IsActiveAsync()
    {
        var activeEntities = await repository.FindAsync(x => x.IsActive, DefaultIncludes());
        return mapper.Map<IEnumerable<TResponse>>(activeEntities);
    }

    public async Task<IEnumerable<TResponse>> IsNotActiveAsync()
    {
        var activeEntities = await repository.FindAsync(x => !x.IsActive, DefaultIncludes());
        return mapper.Map<IEnumerable<TResponse>>(activeEntities);
    }

    public async Task<TEntity> CreateAsync(TCreateRequest request)
    {
        var existsPredicate = IsExistsPredicate(request);
        
        var alreadyExists = await repository.ExistsAsync(existsPredicate);
        if (alreadyExists) 
            throw new ConflictException($"{typeof(TEntity).Name} already exists.");
        
        var entity = mapper.Map<TEntity>(request);
        
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        
        await repository.CreateAsync(entity);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(Guid id, TUpdateRequest request)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) 
            throw new NotFoundException($"{typeof(TEntity).Name} with id '{id}' not found.");
        
        var existsPredicate = UpdateIsExistsPredicate(id, request); 
        var alreadyExists = await repository.ExistsAsync(existsPredicate);
        
        if (alreadyExists) 
            throw new ConflictException($"{typeof(TEntity).Name} already exists.");
        
        mapper.Map(request, entity);
        
        entity.UpdatedAt = DateTime.UtcNow;
        
        await repository.UpdateAsync(entity);
        return entity;
    }
    
    public async Task<int> DeleteAsync(Guid id) => 
        await repository.DeleteAsync(id);
    
    protected virtual Expression<Func<TEntity, bool>> IsExistsPredicate(TCreateRequest request) => 
        _ => false;
    
    protected virtual Expression<Func<TEntity, bool>> UpdateIsExistsPredicate(Guid id, TUpdateRequest request) => 
        _ => false;
    
    protected virtual Expression<Func<TEntity, object>>[] DefaultIncludes() => [];
}
