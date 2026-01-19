using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Services.AssetService;

public interface IAssetService :  IServiceQueryHandler<AssetsModel, AssetsResponse,AssetsCreateRequest,AssetsUpdateRequest>
{
    //Task<IEnumerable<AssetsResponse>> GetMaintenanceDueAsync(Guid institutionId, Guid branchId);
}