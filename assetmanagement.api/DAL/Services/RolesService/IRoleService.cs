using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Services.RolesService;

public interface IRoleService : IServiceQueryHandler<RolesModel, RolesResponse, RolesCreateRequest, RolesUpdateRequest>
{
    HealthResponse GetHealth();
    Task<IEnumerable<RolesResponse>> GetActiveRolesAsync(); 
}