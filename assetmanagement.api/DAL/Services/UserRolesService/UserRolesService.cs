using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Repositories.UserRolesRepository;
using AssetManagement.API.Exceptions;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;
using Serilog;

namespace AssetManagement.API.DAL.Services.UserRolesService;

public class UserRolesService(IUserRoleRepository repo, IMapper mapper) : IUserRolesService
{
    public async Task<IEnumerable<UserRolesResponse>> GetAllAsync()
    {
        var entities = await repo.GetAllAsync(x => x.Roles!,
            x => x.Users!);
        return mapper.Map<IEnumerable<UserRolesResponse>>(entities);
    }

    public async Task<UserRolesResponse?> GetByIdAsync(Guid id)
    {
        var entity = await repo.GetByIdAsync(id, x => x.Roles!,
            x => x.Users!);
        return entity == null ? null : mapper.Map<UserRolesResponse>(entity);
    }

    public async Task<IEnumerable<UserRolesResponse>> GetByUserIdAsync(Guid userId)
    {
        var entities = await repo.GetByUserIdAsync(userId);
        return mapper.Map<IEnumerable<UserRolesResponse>>(entities);
    }

    public async Task<IEnumerable<UserRolesResponse>> GetByRoleIdAsync(Guid roleId)
    {
        var entities = await repo.GetByRoleIdAsync(roleId);
        return mapper.Map<IEnumerable<UserRolesResponse>>(entities);
    }

    public async Task<int> CreateAsync(UserRolesCreateRequest request)
    {
        var exists = await repo.ExistsAsync(x => x.UserId == request.UserId && x.RoleId == request.RoleId);
        if (exists)
            throw new ConflictException($"User {request.UserId} already has role {request.RoleId}.");

        var entity = mapper.Map<UserRolesModel>(request);

        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.IsActive = true;

        var created = await repo.CreateAsync(entity);
        return mapper.Map<int>(created);
    }

    public async Task<int> UpdateAsync(Guid id, UserRolesUpdateRequest request)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing == null)
            throw new NotFoundException($"UserRole with id '{id}' not found.");

        mapper.Map(request, existing);

        existing.UpdatedAt = DateTime.UtcNow;

        var updated = await repo.UpdateAsync(existing);
        return mapper.Map<int>(updated);
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var entity = await repo.GetByIdAsync(id);
        if (entity == null)
            throw new NotFoundException($"UserRole with id '{id}' not found.");

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
}