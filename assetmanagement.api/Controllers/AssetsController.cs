using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Services.AssetService;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController(IAssetService service)
    : BaseApiController<AssetsModel, AssetsResponse, AssetsCreateRequest,
        AssetsUpdateRequest>(service);


// public class AssetsController(IAssetService service) : ControllerBase
// {
//     [HttpGet]
//     [Route(ControllerConstants.HealthRoute)]
//     public HealthResponse GetHealth()
//     {
//         Log.Information("Querying health.");
//         return service.GetHealth();
//     }
//
//     // GET: api/assets
//     [HttpGet]
//     public async Task<ActionResult<IEnumerable<AssetsResponse>>> GetAll()
//     {
//         var assets = await service.GetAllAsync();
//         return Ok(assets);
//     }
//
//     // GET: api/assets/{id}
//     [HttpGet("{id:guid}")]
//     public async Task<ActionResult<AssetsResponse>> GetById(Guid id)
//     {
//         var branch = await service.GetByIdAsync(id);
//         if (branch == null)
//             return NotFound(new { message = $"Asset with id '{id}' not found." });
//
//         return Ok(branch);
//     }
//
//     // GET: api/assets/institution/{institutionId}
//     [HttpGet("institution/{institutionId:guid}")]
//     public async Task<ActionResult<IEnumerable<AssetsResponse>>> GetByInstitutionId(Guid institutionId)
//     {
//         var assets = await service.GetByInstitutionIdAsync(institutionId);
//         return Ok(assets);
//     }
//
//     // POST: api/assets
//     [HttpPost]
//     public async Task<ActionResult<AssetsResponse>> Create(AssetsCreateRequest request)
//     {
//         var created = await service.CreateAsync(request);
//         return Created(nameof(Create), created);
//     }
//
//     // PUT: api/assets/{id}
//     [HttpPut("{id:guid}")]
//     public async Task<ActionResult<AssetsResponse>> Update(Guid id, AssetsUpdateRequest request)
//     {
//         var updated = await service.UpdateAsync(id, request);
//         return NoContent();
//     }
//
//     // DELETE: api/assets/{id}
//     [HttpDelete("{id:guid}")]
//     public async Task<IActionResult> Delete(Guid id)
//     {
//         var deleted = await service.DeleteAsync(id);
//         return NoContent();
//     }
// }