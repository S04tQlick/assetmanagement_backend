using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Services.VendorsService;

public interface IVendorsService : IServiceQueryHandler<VendorsModel, VendorsResponse,VendorsCreateRequest,VendorsUpdateRequest>
{
    HealthResponse GetHealth();
    Task<IEnumerable<VendorsResponse>> GetActiveVendorsAsync();
    Task<IEnumerable<VendorsResponse>> GetByInstitutionIdAsync(Guid institutionId);
}