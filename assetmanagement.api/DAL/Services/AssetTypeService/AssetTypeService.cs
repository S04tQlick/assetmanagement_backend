using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Repositories.AssetTypesRepository;
using AssetManagement.API.Exceptions;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;
using Serilog;

namespace AssetManagement.API.DAL.Services.AssetTypeService;

public class AssetTypeService(IAssetTypeRepository repo, IMapper mapper) : IAssetTypeService
{
    public async Task<IEnumerable<AssetTypesResponse>> GetAllAsync()
    {
        var entities = await repo.GetAllAsync();
        return mapper.Map<IEnumerable<AssetTypesResponse>>(entities);
    }

    public async Task<AssetTypesResponse?> GetByIdAsync(Guid id)
    {
        var entity = await repo.GetByIdAsync(id);
        return entity == null ? null : mapper.Map<AssetTypesResponse>(entity);
    }

    public async Task<IEnumerable<AssetTypesResponse>> GetAllByDateAsync(DateTime date)
    {
        var entities = await repo.FindAsync(e => e.CreatedAt.Date == date.Date);
        return mapper.Map<IEnumerable<AssetTypesResponse>>(entities);
    }

    public async Task<int> CreateAsync(AssetTypesCreateRequest request)
    {
        var exists = await repo.ExistsAsync(x =>
            x.AssetTypeName == request.AssetTypeName  );
        
        if (exists )
            throw new ConflictException($"{request.AssetTypeName} already exists.");

        var entity = mapper.Map<AssetTypesModel>(request);
        entity.IsActive = true;

        var created = await repo.CreateAsync(entity);
        return mapper.Map<int>(created);
    }
    
    public async Task<int> UpdateAsync(Guid id, AssetTypesUpdateRequest request)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing == null)
            throw new NotFoundException($"AssetType with id {id} not found.");

        var exists = await repo.ExistsAsync(x =>
            x.Id != id &&
            x.AssetTypeName == request.AssetTypeName);

        if (exists)
            throw new ConflictException(
                $"{request.AssetTypeName} already exists."
            );

        mapper.Map(request, existing);

        var updated = await repo.UpdateAsync(existing);
        return mapper.Map<int>(updated);
    }

    public async Task<int> DeleteAsync(Guid id)
    {
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

    public async Task<IEnumerable<AssetTypesResponse>> GetActiveAssetTypesAsync()
    {
        var activeEntities = await repo.FindAsync(x => x.IsActive.Equals(true));
        return mapper.Map<IEnumerable<AssetTypesResponse>>(activeEntities);
    }
}