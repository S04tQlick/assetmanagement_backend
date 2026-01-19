using AssetManagement.API.DAL.Services.VendorsService;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class VendorsController(IVendorService service)
    : BaseApiController<VendorsModel, VendorsResponse, VendorsCreateRequest, VendorsUpdateRequest>(service);

// {
//     [HttpGet]
//     [Route(ControllerConstants.HealthRoute)]
//     public HealthResponse GetHealth()
//     {
//         Log.Information("Querying health.");
//         return service.GetHealth();
//     }
//
//     [HttpGet("institution/{institutionId:guid}")]
//     public async Task<ActionResult<IEnumerable<VendorsResponse>>> GetByInstitutionId(Guid institutionId)
//     {
//         var vendors = await service.GetByInstitutionIdAsync(institutionId);
//         return Ok(vendors);
//     }
// }