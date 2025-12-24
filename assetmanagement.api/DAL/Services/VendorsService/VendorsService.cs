using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Repositories.VendorsRepository;
using AssetManagement.API.Exceptions;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;
using Serilog;

namespace AssetManagement.API.DAL.Services.VendorsService;

public class VendorsService(IVendorRepository repo, IMapper mapper) : IVendorsService
{
    public async Task<IEnumerable<VendorsResponse>> GetAllAsync()
    {
        var entities = await repo.GetAllAsync(x=>x.Institutions!);
        return mapper.Map<IEnumerable<VendorsResponse>>(entities);
    }

    public async Task<VendorsResponse?> GetByIdAsync(Guid id)
    {
        var entity = await repo.GetByIdAsync(id, x=>x.Institutions!);
        return entity == null ? null : mapper.Map<VendorsResponse>(entity);
    }

    public async Task<int> CreateAsync(VendorsCreateRequest request)
    {
        if (await repo.ExistsAsync(x =>
                x.VendorsName.Equals(request.VendorsName) &&
                x.InstitutionId.Equals(request.InstitutionId)
            )
           )
            throw new ConflictException(
                $"Branch '{request.VendorsName}' already exists for institution {request.InstitutionId}.");

        var entity = mapper.Map<VendorsModel>(request);
        entity.IsActive = true;

        var created = await repo.CreateAsync(entity);
        return mapper.Map<int>(created);
    }

    public async Task<int> UpdateAsync(Guid id, VendorsUpdateRequest request)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing == null)
            throw new NotFoundException($"Branch with id '{id}' not found.");

        mapper.Map(request, existing);
        existing.UpdatedAt = DateTime.UtcNow;

        var updated = await repo.UpdateAsync(existing);
        return mapper.Map<int>(updated);
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var entity = await repo.GetByIdAsync(id);
        if (entity == null)
            throw new NotFoundException($"Branch with id '{id}' not found.");

        return await repo.DeleteAsync(id);
    }

    public async Task<IEnumerable<VendorsResponse>> GetActiveVendorsAsync()
    {
        var activeEntities = await repo.GetAllAsync();
        return mapper.Map<IEnumerable<VendorsResponse>>(activeEntities);
    }

    public async Task<IEnumerable<VendorsResponse>> GetByInstitutionIdAsync(Guid institutionId)
    {
        var activeEntities = await repo.GetAllAsync();
        return mapper.Map<IEnumerable<VendorsResponse>>(activeEntities);
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