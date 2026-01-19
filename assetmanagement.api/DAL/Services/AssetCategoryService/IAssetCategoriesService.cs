using System.Linq.Expressions;
using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Services.AssetCategoryService;

public interface IAssetCategoriesService : IServiceQueryHandler<AssetCategoriesModel, AssetCategoriesResponse, AssetCategoriesCreateRequest, AssetCategoriesUpdateRequest>
{
    // Task<IEnumerable<AssetCategoriesResponse>> FindAsync(Expression<Func<AssetCategoriesModel, bool>> predicate,
    //     params Expression<Func<AssetCategoriesModel, object>>[]? includes);
}