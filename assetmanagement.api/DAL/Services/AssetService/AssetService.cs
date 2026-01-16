using System.Linq.Expressions;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.API.DAL.Services.AssetService;

public class AssetService(IRepositoryQueryHandler<AssetsModel> repository, IMapper mapper)
    : ServiceQueryHandler<AssetsModel, AssetsResponse, AssetsCreateRequest, AssetsUpdateRequest>(repository, mapper),
        IAssetService
{
    protected override Expression<Func<AssetsModel, bool>> IsExistsPredicate(AssetsCreateRequest request)
    {
        return x =>
            x.AssetName == request.AssetName &&
            x.InstitutionId == request.InstitutionId;
    }

    protected override Expression<Func<AssetsModel, bool>> UpdateIsExistsPredicate(Guid id, AssetsUpdateRequest request)
    {
        return x =>
            x.Id != id &&
            x.AssetName == request.AssetName &&
            x.InstitutionId == request.InstitutionId;
    }
}






// public async Task<IEnumerable<AssetsResponse>> GetMaintenanceDueAsync(Guid institutionId, Guid branchId)
    // {
    //     var dueAssets = await _repository.GetMaintenanceDueAsync(institutionId, branchId);
    //     return _mapper.Map<IEnumerable<AssetsResponse>>(dueAssets);
    // }
    
    
    
    
    
    
    
    
    
    
    
    // public async Task<IEnumerable<AssetsResponse>> GetAllAsync()
    // {
    //     var entities = await repo.GetAllAsync(
    //         x => x.Institutions!,
    //         x => x.Branches!,
    //         x => x.AssetCategories!,
    //         x => x.AssetTypes!,
    //         x => x.Vendors!
    //     );
    //     return _mapper.Map<IEnumerable<AssetsResponse>>(entities);
    // }
    //
    // public async Task<AssetsResponse?> GetByIdAsync(Guid id)
    // {
    //     var entity = await repo.GetByIdAsync(id,
    //         x => x.Institutions!,
    //         x => x.Branches!,
    //         x => x.AssetCategories!,
    //         x => x.AssetTypes!,
    //         x => x.Vendors!
    //     );
    //     return entity == null ? null : _mapper.Map<AssetsResponse>(entity);
    // }