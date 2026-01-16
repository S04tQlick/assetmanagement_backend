using System.Linq.Expressions;
using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.API.DAL.Repositories.RolesRepository;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.API.DAL.Services.RolesService;

public class RoleService(IRoleRepository repository, IMapper mapper) : ServiceQueryHandler<RolesModel, RolesResponse, RolesCreateRequest, RolesUpdateRequest>(repository, mapper), IRoleService
{
    protected override Expression<Func<RolesModel, bool>> IsExistsPredicate(RolesCreateRequest request)
    {
        return x =>
            x.RoleName == request.RoleName;
    }
    
    protected override Expression<Func<RolesModel, bool>> UpdateIsExistsPredicate(Guid id, RolesUpdateRequest request)
    {
        return x =>
            x.Id != id &&
            x.RoleName == request.RoleName ;
    }
}



// {
//     public async Task<IEnumerable<RolesResponse>> GetAllAsync()
//     {
//         var entities = await repo.GetAllAsync();
//         return mapper.Map<IEnumerable<RolesResponse>>(entities);
//     }
//
//     public async Task<RolesResponse?> GetByIdAsync(Guid id)
//     {
//         var entity = await repo.GetByIdAsync(id);
//         return entity == null ? null : mapper.Map<RolesResponse>(entity);
//     }  
//
//     public async Task<RolesModel> CreateAsync(RolesCreateRequest request)
//     {
//         if (await repo.ExistsAsync(x=>x.RoleName.Equals(request.RoleName)))
//             throw new ConflictException($"Role '{request.RoleName}' already exists.");
//
//         var entity = mapper.Map<RolesModel>(request);
//
//         entity.CreatedAt = DateTime.UtcNow;
//         entity.UpdatedAt = DateTime.UtcNow;
//         entity.IsActive = true;
//
//         var created = await repo.CreateAsync(entity);
//         return mapper.Map<int>(created);
//     }
//
//     public async Task<RolesModel> UpdateAsync(Guid id, RolesUpdateRequest request)
//     {
//         var existing = await repo.GetByIdAsync(id);
//         if (existing == null)
//             throw new NotFoundException($"Role with id '{id}' not found.");
//
//         if (await repo.ExistsAsync(x=>x.RoleName.Equals(request.RoleName)) &&
//             existing.RoleName != request.RoleName)
//             throw new ConflictException($"Role '{request.RoleName}' already exists.");
//
//         mapper.Map(request, existing);
//         existing.UpdatedAt = DateTime.UtcNow;
//
//         var updated = await repo.UpdateAsync(existing);
//         return mapper.Map<int>(updated);
//     }
//
//     public async Task<int> DeleteAsync(Guid id)
//     {
//         var entity = await repo.GetByIdAsync(id);
//         if (entity == null)
//             throw new NotFoundException($"Role with id '{id}' not found.");
//
//         return await repo.DeleteAsync(id);
//     } 
//
//     public HealthResponse GetHealth()
//     {
//         Log.Information("Health action queried successfully.");
//         return new HealthResponse
//         {
//             Message = ControllerConstants.HealthMessage,
//             Timestamp = DateTime.UtcNow
//         };
//     }
//
//     public async Task<IEnumerable<RolesResponse>> GetActiveRolesAsync()
//     {
//         var activeEntities = await repo.FindAsync(x => x.IsActive.Equals(true));
//         return mapper.Map<IEnumerable<RolesResponse>>(activeEntities);
//     }
// }