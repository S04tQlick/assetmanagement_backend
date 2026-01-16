using System.Linq.Expressions;
using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.API.DAL.Repositories.BranchesRepository;
using AssetManagement.API.Exceptions;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.API.DAL.Services.BranchesService;

public class BranchesService(IBranchRepository repository, IMapper mapper)
    : ServiceQueryHandler<BranchesModel, BranchesResponse, BranchesCreateRequest, BranchesUpdateRequest>(repository,
        mapper), IBranchesService

{
    private readonly IMapper _mapper = mapper;

    protected override Expression<Func<BranchesModel, object>>[] DefaultIncludes()
    {
        return
        [
            x => x.Institutions!
        ];
    }

    protected override Expression<Func<BranchesModel, bool>> IsExistsPredicate(BranchesCreateRequest request)
    {
        return x =>
            x.BranchName == request.BranchName &&
            x.InstitutionId == request.InstitutionId;
    }

    protected override Expression<Func<BranchesModel, bool>> UpdateIsExistsPredicate(Guid id,
        BranchesUpdateRequest request)
    {
        return x =>
            x.Id != id &&
            x.BranchName == request.BranchName &&
            x.InstitutionId == request.InstitutionId;
    }

    public new async Task<BranchesModel> CreateAsync(BranchesCreateRequest request)
    {
        request.IsActive = true;
        request.IsHeadOffice = await DetermineHeadOfficeFlagAsync(request);
        return await base.CreateAsync(request);
    }

    public new async Task<BranchesModel> UpdateAsync(Guid id, BranchesUpdateRequest request)
    {
        var existing = await repository.GetByIdAsync(id);
        if (existing is null)
            throw new NotFoundException($"Branch with id '{id}' not found.");

        if (request.IsHeadOffice && !existing.IsHeadOffice)
        {
            var hasHeadOffice = await repository.HasHeadOfficeAsync(existing.InstitutionId);
            if (hasHeadOffice)
                throw new ConflictException($"Institution {existing.InstitutionId} already has a Head Office.");
        }

        if (await BranchExistsAsync(request.InstitutionId, request.BranchName, request.Latitude, request.Longitude))
            throw new ConflictException($"Branch {request.BranchName} already exists at this location.");

        _mapper.Map(request, existing);

        existing.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(existing);

        return existing;
    }

    private async Task<bool> DetermineHeadOfficeFlagAsync(BranchesCreateRequest request)
    {
        var existingBranches = await repository.GetByInstitutionIdAsync(request.InstitutionId);
        var institutionHasAnyBranches = existingBranches.Any();
        var institutionAlreadyHasHeadOffice = await repository.HasHeadOfficeAsync(request.InstitutionId);

        return DetermineHeadOfficeFlag(isExplicitHeadOfficeRequest: request.IsHeadOffice,
            institutionHasAnyBranches: institutionHasAnyBranches,
            institutionAlreadyHasHeadOffice: institutionAlreadyHasHeadOffice);
    }

    private async Task<bool> BranchExistsAsync(Guid institutionId, string branchName, double latitude, double longitude)
    {
        const double tolerance = 0.000001;

        return await repository.ExistsAsync(b =>
            b.InstitutionId == institutionId &&
            b.BranchName.ToLower().Equals(branchName.ToLower()) &&
            Math.Abs(b.Latitude - latitude) < tolerance &&
            Math.Abs(b.Longitude - longitude) < tolerance
        );
    }

    private static bool DetermineHeadOfficeFlag(bool isExplicitHeadOfficeRequest, bool institutionHasAnyBranches,
        bool institutionAlreadyHasHeadOffice)
    {
        // First branch → automatically head office
        if (!institutionHasAnyBranches)
            return true;

        // If user did NOT request head office → false
        if (!isExplicitHeadOfficeRequest)
            return false;

        // If user DID request head office but one already exists → conflict
        return institutionAlreadyHasHeadOffice
            ? throw new ConflictException("Institution already has a Head Office.")
            :
            // Otherwise → allow head office
            true;
    }
}