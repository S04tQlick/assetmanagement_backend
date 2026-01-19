using System.Linq.Expressions;
using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.API.DAL.Repositories.UserRolesRepository;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.API.DAL.Services.UserRolesService;

public class UserRoleService(IUserRoleRepository repository, IMapper mapper) : ServiceQueryHandler<UserRolesModel, UserRolesResponse, UserRolesCreateRequest, UserRolesUpdateRequest>(repository, mapper), IUserRoleService
{
    public async Task<IEnumerable<UserRolesResponse>> GetByUserIdAsync(Guid userId)
    {
        var entities = await repository.GetByUserIdAsync(userId);
        return mapper.Map<IEnumerable<UserRolesResponse>>(entities);
    }

    public async Task<IEnumerable<UserRolesResponse>> GetByRoleIdAsync(Guid roleId)
    {
        var entities = await repository.GetByRoleIdAsync(roleId);
        return mapper.Map<IEnumerable<UserRolesResponse>>(entities); 
    }
    protected override Expression<Func<UserRolesModel, object>>[] DefaultIncludes()
    {
        return
        [
            x => x.Roles!,
            x => x.Users!
        ];
    }
     protected override Expression<Func<UserRolesModel, bool>> IsExistsPredicate(UserRolesCreateRequest request)
     {
         return x =>  
             x.UserId == request.UserId && 
             x.RoleId == request.RoleId;
     }

     protected override Expression<Func<UserRolesModel, bool>> UpdateIsExistsPredicate(Guid id, UserRolesUpdateRequest request)
     {
         return x => 
             x.Id != id && 
             x.UserId == request.UserId && 
             x.RoleId == request.RoleId;
     }

}







     //
     //
     //
     // public new async Task<UserRolesModel> CreateAsync(UserRolesCreateRequest request)
     // {
     //     var existsPredicate = CreateExistsPredicate(request);
     //     
     //     var alreadyExists  = await _repository.ExistsAsync(existsPredicate);
     //     if (alreadyExists )
     //         throw new ConflictException($"User {request.UserId} already has role {request.RoleId}.");
     //     
     //     request.IsActive = true;
     //
     //     return await base.CreateAsync(request);
     // }

// public async Task<IEnumerable<UserRolesResponse>> GetByUserIdAsync(Guid userId)
//      {
//          var entities = await _repository.GetByUserIdAsync(userId);
//          return _mapper.Map<IEnumerable<UserRolesResponse>>(entities);
//      }
//
//      public async Task<IEnumerable<UserRolesResponse>> GetByRoleIdAsync(Guid roleId)
//      {
//          var entities = await _repository.GetByRoleIdAsync(roleId);
//          return _mapper.Map<IEnumerable<UserRolesResponse>>(entities);
//      }





















// public class UserRolesService(IRepositoryQueryHandler<UsersModel> repository, IMapper mapper) : ServiceQueryHandler<UsersModel, UsersResponse, UsersCreateRequest, UsersUpdateRequest>(repository, mapper),IUserRolesService
// {
//     private readonly IRepositoryQueryHandler<UsersModel> _repository = repository;
//     private readonly IMapper _mapper = mapper;
//
//     public new async Task<IEnumerable<UserRolesResponse>> GetAllAsync()
//     {
//         var entities = await _repository.GetAllAsync(x => x.Roles!,
//             x => x.Users!);
//         return _mapper.Map<IEnumerable<UserRolesResponse>>(entities);
//     }
//
//     public async Task<UserRolesResponse?> GetByIdAsync(Guid id)
//     {
//         var entity = await _repository.GetByIdAsync(id, x => x.Roles!,
//             x => x.Users!);
//         return entity == null ? null : _mapper.Map<UserRolesResponse>(entity);
//     }
//
//     public Task<IEnumerable<UserRolesResponse>> GetByInstitutionIdAsync(Guid institutionId)
//     {
//         throw new NotImplementedException();
//     }
//
//     public async Task<IEnumerable<UserRolesResponse>> GetByUserIdAsync(Guid userId)
//     {
//         var entities = await _repository.GetByUserIdAsync(userId);
//         return _mapper.Map<IEnumerable<UserRolesResponse>>(entities);
//     }
//
//     public async Task<IEnumerable<UserRolesResponse>> GetByRoleIdAsync(Guid roleId)
//     {
//         var entities = await _repository.GetByRoleIdAsync(roleId);
//         return _mapper.Map<IEnumerable<UserRolesResponse>>(entities);
//     }
//
//     public async Task<UserRolesModel> CreateAsync(UserRolesCreateRequest request)
//     {
//         var exists = await _repository.ExistsAsync(x => x.UserId == request.UserId && x.RoleId == request.RoleId);
//         if (exists)
//             throw new ConflictException($"User {request.UserId} already has role {request.RoleId}.");
//
//         var entity = _mapper.Map<UserRolesModel>(request);
//
//         entity.CreatedAt = DateTime.UtcNow;
//         entity.UpdatedAt = DateTime.UtcNow;
//         entity.IsActive = true;
//
//         var created = await _repository.CreateAsync(entity);
//         return _mapper.Map<UserRolesModel>(created);
//     }
//
//     public async Task<UserRolesModel> UpdateAsync(Guid id, UserRolesUpdateRequest request)
//     {
//         var existing = await _repository.GetByIdAsync(id);
//         if (existing == null)
//             throw new NotFoundException($"UserRole with id '{id}' not found.");
//
//         _mapper.Map(request, existing);
//
//         existing.UpdatedAt = DateTime.UtcNow;
//
//         var updated = await _repository.UpdateAsync(existing);
//         return _mapper.Map<UserRolesModel>(updated);
//     }
//
//     public async Task<int> DeleteAsync(Guid id)
//     {
//         var entity = await _repository.GetByIdAsync(id);
//         if (entity == null)
//             throw new NotFoundException($"UserRole with id '{id}' not found.");
//
//         return await _repository.DeleteAsync(id);
//     }
// }