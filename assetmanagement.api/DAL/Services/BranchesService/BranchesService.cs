using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Repositories.BranchesRepository;
using AssetManagement.API.Exceptions;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper; 
using Serilog;

namespace AssetManagement.API.DAL.Services.BranchesService;

public class BranchesService(IBranchRepository repo, IMapper mapper) : IBranchesService
{
    public async Task<IEnumerable<BranchesResponse>> GetAllAsync()
    {
        var entities = await repo.GetAllAsync(x => x.Institutions!);
        return mapper.Map<IEnumerable<BranchesResponse>>(entities);
    }

    public async Task<BranchesResponse?> GetByIdAsync(Guid id)
    {
        var entity = await repo.GetByIdAsync(
            id,
            x => x.Institutions!
        );
        return entity == null ? null : mapper.Map<BranchesResponse>(entity);
    }

    public async Task<IEnumerable<BranchesResponse>> GetByInstitutionIdAsync(Guid institutionId)
    {
        var entities = await repo.GetByInstitutionIdAsync(institutionId);
        return mapper.Map<IEnumerable<BranchesResponse>>(entities);
    }

    public async Task<int> CreateAsync(BranchesCreateRequest request)
    {
        if (await repo.ExistsAsync(x =>
                x.BranchName.Equals(request.BranchName) && x.InstitutionId.Equals(request.InstitutionId)))
            throw new ConflictException(
                $"Branch '{request.BranchName}' already exists for institution {request.InstitutionId}.");

        var entity = mapper.Map<BranchesModel>(request);
        entity.IsActive = true;

        entity.IsHeadOffice = await DetermineHeadOfficeFlagAsync(request);

        var created = await repo.CreateAsync(entity);
        return mapper.Map<int>(created);
    }

    public async Task<int> UpdateAsync(Guid id, BranchesUpdateRequest request)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing is null)
            throw new NotFoundException($"Branch with id '{id}' not found.");

        if (request.IsHeadOffice && !existing.IsHeadOffice)
        {
            var hasHeadOffice = await repo.HasHeadOfficeAsync(existing.InstitutionId);
            if (hasHeadOffice)
                throw new ConflictException($"Institution {existing.InstitutionId} already has a Head Office.");
        }

        if (await BranchExistsAsync(request.InstitutionId, request.BranchName, request.Latitude, request.Longitude))
            throw new ConflictException($"Branch {request.BranchName} already exists at this location.");

        mapper.Map(request, existing);

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

    public async Task<IEnumerable<BranchesResponse>> GetActiveBranchesAsync()
    {
        var activeEntities = await repo.FindAsync(x => x.IsActive.Equals(true));
        return mapper.Map<IEnumerable<BranchesResponse>>(activeEntities);
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

    private async Task<bool> DetermineHeadOfficeFlagAsync(BranchesCreateRequest request)
    {
        var existingBranches = await repo.GetByInstitutionIdAsync(request.InstitutionId);

        // First branch → head office
        if (!existingBranches.Any())
            return true;

        // Explicit head office request → check conflicts
        if (!request.IsHeadOffice) return false;
        var hasHeadOffice = await repo.HasHeadOfficeAsync(request.InstitutionId);
        if (hasHeadOffice)
        {
            throw new ConflictException(
                $"Institution {request.InstitutionId} already has a Head Office.");
        }

        return true;

        // Default case → not head office
    }

    private async Task<bool> BranchExistsAsync(Guid institutionId, string branchName, double latitude, double longitude)
    {
        const double tolerance = 0.000001;

        return await repo.ExistsAsync(b =>
            b.InstitutionId == institutionId &&
            b.BranchName.ToLower().Equals(branchName.ToLower()) &&
            Math.Abs(b.Latitude - latitude) < tolerance &&
            Math.Abs(b.Longitude - longitude) < tolerance
        );
    }
}