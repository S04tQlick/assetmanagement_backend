using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Services.BranchesService;

public interface IBranchesService : IServiceQueryHandler<BranchesModel, BranchesResponse, BranchesCreateRequest,
    BranchesUpdateRequest>;

// {
//     HealthResponse GetHealth();
//     Task<IEnumerable<BranchesResponse>> GetActiveBranchesAsync();
//     Task<IEnumerable<BranchesResponse>> GetByInstitutionIdAsync(Guid institutionId);
// }