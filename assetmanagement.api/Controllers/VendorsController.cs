using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Services.VendorsService;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AssetManagement.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class VendorsController(IVendorsService service) : ControllerBase
{
    [HttpGet]
    [Route(ControllerConstants.HealthRoute)]
    public HealthResponse GetHealth()
    {
        Log.Information("Querying health.");
        return service.GetHealth();
    }

    // GET: api/vendors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VendorsResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var vendors = await service.GetAllAsync();
        return Ok(vendors);
    }

    // GET: api/vendors/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VendorsResponse>> GetById(Guid id)
    {
        var branch = await service.GetByIdAsync(id);
        if (branch == null)
            return NotFound(new { message = $"Branch with id '{id}' not found." });

        return Ok(branch);
    }

    // GET: api/vendors/institution/{institutionId}
    [HttpGet("institution/{institutionId:guid}")]
    public async Task<ActionResult<IEnumerable<VendorsResponse>>> GetByInstitutionId(Guid institutionId)
    {
        var vendors = await service.GetByInstitutionIdAsync(institutionId);
        return Ok(vendors);
    }

    // POST: api/vendors
    [HttpPost]
    public async Task<ActionResult<VendorsResponse>> Create(VendorsCreateRequest request)
    {
        var created = await service.CreateAsync(request);
        return Created(nameof(Create), created);
        //return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/vendors/{id}
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<VendorsResponse>> Update(Guid id, VendorsUpdateRequest request)
    {
        var updated = await service.UpdateAsync(id, request);
        if (updated == 0)
            return NotFound(new { message = $"Branch with id '{id}' not found." });

        return NoContent();
    }

    // DELETE: api/vendors/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await service.DeleteAsync(id);
        if (deleted == 0)
            return NotFound(new { message = $"Branch with id '{id}' not found." });

        return NoContent();
    }
}