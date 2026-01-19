using System.Linq.Expressions;
using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.API.DAL.Repositories.InstitutionsRepository;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.API.DAL.Services.InstitutionService;

public class InstitutionService(IInstitutionRepository repository, IMapper mapper) : ServiceQueryHandler<InstitutionsModel, InstitutionsResponse, InstitutionsCreateRequest, InstitutionsUpdateRequest>(repository, mapper), IInstitutionService
{
    protected override Expression<Func<InstitutionsModel, bool>> IsExistsPredicate(InstitutionsCreateRequest request)
    {
        return x =>
            x.InstitutionName == request.InstitutionName;
    }
    
    protected override Expression<Func<InstitutionsModel, bool>> UpdateIsExistsPredicate(Guid id, InstitutionsUpdateRequest request)
    {
        return x =>
            x.Id != id &&
            x.InstitutionName == request.InstitutionName ;
    }
}


  
//     public async Task<IEnumerable<InstitutionsResponse>> GetByDateAsync(DateTime date)
//     {
//         var entities = await repo.GetByDateAsync(date);
//         return mapper.Map<IEnumerable<InstitutionsResponse>>(entities);
//     }
//
//     public async Task<IEnumerable<InstitutionsResponse>> GetActiveInstitutionsAsync( )
//     {
//         var entities = await repo.GetActiveInstitutionsAsync();
//         return mapper.Map<IEnumerable<InstitutionsResponse>>(entities);
//     }
//
//     public async Task<IEnumerable<InstitutionsResponse>> GetByEmailAsync(string email)
//     {
//         var entities = await repo.GetByEmailAsync(email);
//         return mapper.Map<IEnumerable<InstitutionsResponse>>(entities);
//     }
//
//     public async Task<InstitutionsModel> CreateAsync(InstitutionsCreateRequest request)
//     {
//         if (await repo.ExistsAsync(x=>x.InstitutionName == request.InstitutionName))
//             throw new ConflictException($"Institution with name '{request.InstitutionName}' already exists."); 
//
//         var entity = mapper.Map<InstitutionsModel>(request);
//         entity.IsActive = true;
//
//         var created = await repo.CreateAsync(entity);
//         return mapper.Map<int>(created);
//     }
//
//     public async Task<InstitutionsModel> UpdateAsync(Guid id, InstitutionsUpdateRequest request)
//     {
//         var existing = await repo.GetByIdAsync(id);
//         if (existing == null)
//             throw new NotFoundException($"Institution with id '{id}' not found.");
//
//         if (await repo.ExistsAsync(x=>x.InstitutionName == request.InstitutionName) && existing.InstitutionName != request.InstitutionName)
//             throw new ConflictException($"Institution with name '{request.InstitutionName}' already exists.");
//
//         mapper.Map(request, existing);
//
//         var updated = await repo.UpdateAsync(existing);
//         return mapper.Map<int>(updated);
//     }
//
//     public async Task<int> DeleteAsync(Guid id)
//     {
//         var entity = await repo.GetByIdAsync(id);
//         if (entity == null)
//             throw new NotFoundException($"Institution with id '{id}' not found.");
//
//         return await repo.DeleteAsync(id);
//     }
//
//     public async Task<IEnumerable<InstitutionsResponse>> GetActiveInstitutionAsync(
//         CancellationToken cancellationToken = default)
//     {
//         var activeEntities = await repo.FindAsync(x => x.IsActive.Equals(true));
//         return mapper.Map<IEnumerable<InstitutionsResponse>>(activeEntities);
//     }
//
//     public async Task<bool> DisableInstitutionAsync(Guid id)
//     {
//         var entity = await repo.GetByIdAsync(id);
//         if (entity == null) return false;
//
//         entity.IsActive = false;
//         
//         await repo.UpdateAsync(entity);
//         return true;
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
// }
