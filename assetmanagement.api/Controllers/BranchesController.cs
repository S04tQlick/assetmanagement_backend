using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Services.BranchesService;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BranchesController(IBranchesService service) : ControllerBase
{
    [HttpGet]
    [Route(ControllerConstants.HealthRoute)]
    public HealthResponse GetHealth()
    {
        Log.Information("Querying health.");
        return service.GetHealth();
    }

    // GET: api/branches
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BranchesResponse>>> GetAll()
    {
        var branches = await service.GetAllAsync();
        return Ok(branches);
    }

    // GET: api/branches/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BranchesResponse>> GetById(Guid id)
    {
        var branch = await service.GetByIdAsync(id);
        if (branch == null)
            return NotFound(new { message = $"Branch with id '{id}' not found." });

        return Ok(branch);
    }

    // GET: api/branches/institution/{institutionId}
    [HttpGet("institution/{institutionId:guid}")]
    public async Task<ActionResult<IEnumerable<BranchesResponse>>> GetByInstitutionId(Guid institutionId)
    {
        var branches = await service.GetByInstitutionIdAsync(institutionId);
        return Ok(branches);
    }

    // POST: api/branches
    [HttpPost]
    public async Task<ActionResult<BranchesResponse>> Create(BranchesCreateRequest request)
    {
        var created = await service.CreateAsync(request);
        return Created(nameof(Create), created);
        //return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/branches/{id}
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<BranchesResponse>> Update(Guid id, BranchesUpdateRequest request)
    {
        var updated = await service.UpdateAsync(id, request);
        if (updated == 0)
            return NotFound(new { message = $"Branch with id '{id}' not found." });

        return NoContent();
    }

    // DELETE: api/branches/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await service.DeleteAsync(id);
        if (deleted == 0)
            return NotFound(new { message = $"Branch with id '{id}' not found." });

        return NoContent();
    }
}