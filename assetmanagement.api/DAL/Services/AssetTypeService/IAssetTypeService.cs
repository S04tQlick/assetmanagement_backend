using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Services.AssetTypeService;

public interface IAssetTypeService : IServiceQueryHandler<AssetTypesModel, AssetTypesResponse, AssetTypesCreateRequest, AssetTypesUpdateRequest>
{
    HealthResponse GetHealth();
    Task<IEnumerable<AssetTypesResponse>> GetActiveAssetTypesAsync();
}