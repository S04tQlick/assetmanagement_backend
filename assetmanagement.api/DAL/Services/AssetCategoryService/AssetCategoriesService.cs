using System.Linq.Expressions;
using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Repositories.AssetCategoriesRepository;
using AssetManagement.API.Exceptions;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;
using Serilog;

namespace AssetManagement.API.DAL.Services.AssetCategoryService;

public class AssetCategoriesService(IAssetCategoryRepository repo, IMapper mapper) : IAssetCategoriesService
{
    public async Task<IEnumerable<AssetCategoriesResponse>> GetAllAsync()
    {
        var entities = await repo.GetAllAsync(
            x => x.Institutions!,
            x => x.AssetTypes!
        );

        return mapper.Map<IEnumerable<AssetCategoriesResponse>>(entities);
    }

    public async Task<AssetCategoriesResponse?> GetByIdAsync(Guid id)
    {
        var entity = await repo.GetByIdAsync(id,
            x => x.Institutions!,
            x => x.AssetTypes!
        );

        return entity == null ? null : mapper.Map<AssetCategoriesResponse>(entity);
    }

    public async Task<int> CreateAsync(AssetCategoriesCreateRequest request)
    {
        var exists = await repo.ExistsAsync(x =>
            x.AssetCategoryName == request.AssetCategoryName &&
            x.InstitutionId == request.InstitutionId);
        
        if (exists )
            throw new ConflictException($"{request.AssetCategoryName} already exists.");

        var entity = mapper.Map<AssetCategoriesModel>(request);
        entity.IsActive = true;

        var created = await repo.CreateAsync(entity);
        return mapper.Map<int>(created);
    }

    public async Task<int> UpdateAsync(Guid id, AssetCategoriesUpdateRequest request)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing == null)
            throw new NotFoundException($"AssetCategory with id '{id}' not found.");

        var exists = await repo.ExistsAsync(x =>
            x.Id != id &&
            x.AssetCategoryName == request.AssetCategoryName &&
            x.InstitutionId == request.InstitutionId);

        if (exists)
            throw new ConflictException(
                $"{request.AssetCategoryName} already exists for institution {request.InstitutionId}."
            );

        mapper.Map(request, existing);

        var updated = await repo.UpdateAsync(existing);
        return mapper.Map<int>(updated);
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var entity = await repo.GetByIdAsync(id);
        if (entity == null)
            throw new NotFoundException($"AssetCategory with id '{id}' not found.");
    
        return await repo.DeleteAsync(id);
    }

    public HealthResponse GetHealth()
    {
        Log.Information("Health action queried successfully.");
        return new HealthResponse
        {
            Message = ControllerConstants.HealthMessage,
            Timestamp = DateTime.UtcNow
        };
    } 
    
    public async Task<IEnumerable<AssetCategoriesResponse>> FindAsync(
        Expression<Func<AssetCategoriesModel, bool>> predicate,
        params Expression<Func<AssetCategoriesModel, object>>[]? includes)
    {
        var entities = await repo.FindAsync(predicate, includes);
        return mapper.Map<IEnumerable<AssetCategoriesResponse>>(entities);
    }
}