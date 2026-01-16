using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Services.UsersService;

public interface
    IUsersService : IServiceQueryHandler<UsersModel, UsersResponse, UsersCreateRequest, UsersUpdateRequest>;
// { 
//     Task<IEnumerable<UsersResponse>> GetActiveUsersAsync();
//     Task<UsersResponse?> GetByEmailAsync(string email);
// } 