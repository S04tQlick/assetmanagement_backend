using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Repositories.AssetsRepository;
using AssetManagement.API.Exceptions;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;
using Serilog;

namespace AssetManagement.API.DAL.Services.AssetService;

public class AssetService(IAssetRepository repo, IMapper mapper) : IAssetService
{
    public async Task<IEnumerable<AssetsResponse>> GetAllAsync()
    {
        var entities = await repo.GetAllAsync(
            x => x.Institutions!,
            x => x.Branches!,
            x => x.AssetCategories!,
            x => x.AssetTypes!,
            x => x.Vendors!
        );
        return mapper.Map<IEnumerable<AssetsResponse>>(entities);
    }

    public async Task<AssetsResponse?> GetByIdAsync(Guid id)
    {
        var entity = await repo.GetByIdAsync(id,
            x => x.Institutions!,
            x => x.Branches!,
            x => x.AssetCategories!,
            x => x.AssetTypes!,
            x => x.Vendors!
        );
        return entity == null ? null : mapper.Map<AssetsResponse>(entity);
    }

    public async Task<IEnumerable<AssetsResponse>> GetByInstitutionIdAsync(Guid institutionId)
    {
        var entities = await repo.GetByInstitutionIdAsync(institutionId);
        return mapper.Map<IEnumerable<AssetsResponse>>(entities);
    }

    public async Task<int> CreateAsync(AssetsCreateRequest request)
    {
        if (await repo.ExistsAsync(x => x.AssetName.Equals(request.AssetName) && x.InstitutionId.Equals(request.InstitutionId)))
            throw new ConflictException(
                $"Asset '{request.AssetName}' already exists for institution {request.InstitutionId}.");

        var entity = mapper.Map<AssetsModel>(request);
        entity.IsActive = true;

        var created = await repo.CreateAsync(entity);
        return mapper.Map<int>(created);
    }

    public async Task<int> UpdateAsync(Guid id, AssetsUpdateRequest request)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing == null)
            throw new NotFoundException($"Asset with id '{id}' not found.");

        mapper.Map(request, existing);

        var updated = await repo.UpdateAsync(existing);
        return mapper.Map<int>(updated);
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var entity = await repo.GetByIdAsync(id);
        if (entity == null)
            throw new NotFoundException($"Asset with id '{id}' not found.");

        return await repo.DeleteAsync(id);
    }

    public async Task<IEnumerable<AssetsResponse>> GetActiveAssetsAsync()
    {
        var activeEntities = await repo.FindAsync(x => x.IsActive.Equals(true));
        return mapper.Map<IEnumerable<AssetsResponse>>(activeEntities);
    }

    public async Task<IEnumerable<AssetsResponse>> GetMaintenanceDueAsync(Guid institutionId, Guid branchId)
    {
        var dueAssets = await repo.GetMaintenanceDueAsync(institutionId, branchId);
        return mapper.Map<IEnumerable<AssetsResponse>>(dueAssets);
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


}