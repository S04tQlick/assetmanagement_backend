using System.Linq.Expressions;
using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.API.DAL.Repositories.AssetCategoriesRepository;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.API.DAL.Services.AssetCategoryService;

public class AssetCategoriesService(IAssetCategoryRepository repository, IMapper mapper) : ServiceQueryHandler<AssetCategoriesModel, AssetCategoriesResponse, AssetCategoriesCreateRequest, AssetCategoriesUpdateRequest>(repository, mapper), IAssetCategoriesService
{
    protected override Expression<Func<AssetCategoriesModel, object>>[] DefaultIncludes()
    {
        return
        [
            x => x.AssetTypes!,
            x => x.Institutions!
        ];
    }
    
    protected override Expression<Func<AssetCategoriesModel, bool>> IsExistsPredicate(AssetCategoriesCreateRequest request)
    {
        return  x =>
            x.AssetCategoryName == request.AssetCategoryName &&
            x.InstitutionId == request.InstitutionId;
    }
    
    protected override Expression<Func<AssetCategoriesModel, bool>> UpdateIsExistsPredicate(Guid id, AssetCategoriesUpdateRequest request)
    {
        return  x =>
            x.Id != id &&
            x.AssetCategoryName == request.AssetCategoryName &&
            x.InstitutionId == request.InstitutionId;
    }
}