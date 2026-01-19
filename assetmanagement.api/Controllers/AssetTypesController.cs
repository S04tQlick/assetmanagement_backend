using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Services.AssetTypeService;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetTypesController(IAssetTypeService service)
    : BaseApiController<AssetTypesModel, AssetTypesResponse, AssetTypesCreateRequest, AssetTypesUpdateRequest>(service);
// {
//     [HttpGet]
//     [Route(ControllerConstants.HealthRoute)]
//     public HealthResponse GetHealth()
//     {
//         Log.Information("Querying health.");
//         return service.GetHealth();
//     }
//  
//     // [HttpGet("active")]
//     // public async Task<ActionResult<IEnumerable<AssetTypesResponse>>> GetActive()
//     // {
//     //     Log.Information("Fetching active asset types");
//     //     return Ok(await service.GetActiveAssetTypesAsync());
//     // }
//     //
//     // [HttpGet("in-active")]
//     // public async Task<ActionResult<IEnumerable<AssetTypesResponse>>> GetInActive()
//     // {
//     //     Log.Information("Fetching active asset types");
//     //     return Ok(await service.GetInActiveAssetTypesAsync());
//     // }
// }
