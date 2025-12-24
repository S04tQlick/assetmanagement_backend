using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Services.InstitutionService;

public interface IInstitutionService : IServiceQueryHandler<InstitutionsModel, InstitutionsResponse, InstitutionsCreateRequest, InstitutionsUpdateRequest>
{
    Task<IEnumerable<InstitutionsResponse>> GetByDateAsync(DateTime date);
    Task<IEnumerable<InstitutionsResponse>> GetActiveInstitutionAsync(CancellationToken cancellationToken = default);
    Task<bool> DisableInstitutionAsync(Guid id); 
    HealthResponse GetHealth();
}