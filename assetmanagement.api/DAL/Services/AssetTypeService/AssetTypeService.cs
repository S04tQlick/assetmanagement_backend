using System.Linq.Expressions;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.API.DAL.Repositories.AssetTypesRepository;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.API.DAL.Services.AssetTypeService;


public class AssetTypeService(IAssetTypeRepository repository, IMapper mapper) : ServiceQueryHandler<AssetTypesModel, AssetTypesResponse, AssetTypesCreateRequest, AssetTypesUpdateRequest>(repository, mapper), IAssetTypeService
{
    protected override Expression<Func<AssetTypesModel, bool>> IsExistsPredicate(AssetTypesCreateRequest request)
    {
        return x =>
            x.AssetTypeName == request.AssetTypeName;
    }

    protected override Expression<Func<AssetTypesModel, bool>> UpdateIsExistsPredicate(Guid id, AssetTypesUpdateRequest request)
    {
        return x =>
            x.Id != id &&
            x.AssetTypeName == request.AssetTypeName;
    }
}