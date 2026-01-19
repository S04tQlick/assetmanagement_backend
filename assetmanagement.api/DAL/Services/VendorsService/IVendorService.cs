using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Services.VendorsService;

public interface IVendorService : IServiceQueryHandler<VendorsModel, VendorsResponse, VendorsCreateRequest, VendorsUpdateRequest>;
// {
//     Task<IEnumerable<VendorsResponse>> GetActiveVendorsAsync();
// }