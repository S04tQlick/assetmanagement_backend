using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Services.UserRolesService;

public interface IUserRoleService : IServiceQueryHandler<UserRolesModel, UserRolesResponse, UserRolesCreateRequest, UserRolesUpdateRequest>
{
    Task<IEnumerable<UserRolesResponse>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<UserRolesResponse>> GetByRoleIdAsync(Guid roleId); 
} 