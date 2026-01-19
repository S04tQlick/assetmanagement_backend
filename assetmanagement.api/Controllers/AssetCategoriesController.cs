using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Services.AssetCategoryService;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//public class AssetCategoriesController(IAssetCategoriesService service) : BaseApiController<AssetCategoriesModel, AssetCategoriesResponse, AssetCategoriesCreateRequest, AssetCategoriesUpdateRequest>(service)
public class AssetCategoriesController(IAssetCategoriesService service)
    : BaseApiController<AssetCategoriesModel, AssetCategoriesResponse, AssetCategoriesCreateRequest,
        AssetCategoriesUpdateRequest>(service);
// {
//     [HttpGet]
//     [Route(ControllerConstants.HealthRoute)]
//     public HealthResponse GetHealth()
//     {
//         Log.Information("Querying health.");
//         return service.GetHealth();
//     }
//
//     // [HttpGet("{institutionId:guid}/{assetTypeId:guid}")]
//     // public async Task<IEnumerable<AssetCategoriesResponse>> GetByInstitutionAndType(Guid institutionId, Guid assetTypeId)
//     // {
//     //     return await service.FindAsync(x => x.InstitutionId.Equals(institutionId),
//     //         x => x.AssetTypeId.Equals(assetTypeId));
//     // }
// }













// [ApiController]
// [Route("api/[controller]")]
// public class AssetCategoriesController(IAssetCategoriesService service) : ControllerBase
// {
//     [HttpGet]
//     [Route(ControllerConstants.HealthRoute)]
//     public HealthResponse GetHealth()
//     {
//         Log.Information("Querying health.");
//         return service.GetHealth();
//     }
//
//     // GET: api/assetcategories
//     [HttpGet]
//     public async Task<ActionResult<IEnumerable<AssetCategoriesResponse>>> GetAll()
//     {
//         
//         
//         var categories = await service.GetAllAsync();
//         return Ok(categories);
//     }
//
//     // GET: api/assetcategories/{id}
//     [HttpGet("{id:guid}")]
//     public async Task<ActionResult<AssetCategoriesResponse>> GetById(Guid id)
//     {
//         var category = await service.GetByIdAsync(id,
//             x => x.AssetType!);
//         if (category == null)
//             return NotFound(new { message = $"AssetCategory with id '{id}' not found." });
//
//         return Ok(category);
//     }
//
//     // GET: api/assetcategories/institution/{institutionId}
//     [HttpGet("institution/{institutionId:Guid}/type/{typeId:Guid}")]
//     public async Task<ActionResult<IEnumerable<AssetCategoriesResponse>>> GetByInstitutionIdAndTypeId(
//         Guid institutionId, Guid typeId)
//     {
//         var categories = await service.GetByInstitutionIdAndAssetTypeIdAsync(institutionId, typeId);
//         return Ok(categories);
//     }
//
//     // POST: api/assetcategories
//     [HttpPost]
//     public async Task<ActionResult<AssetCategoriesResponse>> Create(AssetCategoriesCreateRequest request)
//     {
//         var created = await service.CreateAsync(request);
//         return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
//     }
//
//     // PUT: api/assetcategories/{id}
//     [HttpPut("{id:guid}")]
//     public async Task<ActionResult<AssetCategoriesResponse>> Update(Guid id, AssetCategoriesUpdateRequest request)
//     {
//         var updated = await service.UpdateAsync(id, request);
//         if (updated == null)
//             return NotFound(new { message = $"AssetCategory with id '{id}' not found." });
//
//         return NoContent();
//     }
//
//     // DELETE: api/assetcategories/{id}
//     [HttpDelete("{id:guid}")]
//     public async Task<IActionResult> Delete(Guid id)
//     {
//         var deleted = await service.DeleteAsync(id);
//         if (deleted == 0)
//             return NotFound(new { message = $"AssetCategory with id '{id}' not found." });
//
//         return NoContent();
//     }
// }