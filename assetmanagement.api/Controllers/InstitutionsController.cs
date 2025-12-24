using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Services.InstitutionService;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstitutionsController (IInstitutionService service ) : BaseApiController<InstitutionsModel, InstitutionsResponse, InstitutionsCreateRequest, InstitutionsUpdateRequest>(service)
{
    [HttpGet]
    [Route(ControllerConstants.HealthRoute)]
    public HealthResponse GetHealth()
    {
        Log.Information("Querying health.");
        return service.GetHealth();
    }

    // // GET: api/asset-types/active
    // [HttpGet("active")]
    // public async Task<ActionResult<IEnumerable<AssetTypesResponse>>> GetActive()
    // {
    //     Log.Information("Fetching active asset types");
    //     return Ok(await service.GetActiveInstitutionAsync());
    // }
    //
    // [HttpGet]
    // [Route(ControllerConstants.GetByDateRoute)]
    // public async Task<ActionResult<IEnumerable<InstitutionsResponse>>> GetByDate(DateTime date )
    // {
    //     var institutions = await service.GetByDateAsync(date);
    //     return Ok(institutions);
    // }    
    //
    // // DELETE: api/institutions/{id}
    // [HttpPatch("{id:guid}")]
    // public async Task<IActionResult> Disable(Guid id)
    // {
    //     Log.Information("Disabling institution with Id {Id}", id);
    //     var success = await service.DisableInstitutionAsync(id);
    //
    //     if (!success)
    //         return NotFound();
    //
    //     return NoContent();
    // }
}