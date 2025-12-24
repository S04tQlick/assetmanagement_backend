using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Services.RolesService;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController(IRoleService service) : ControllerBase
{
    [HttpGet]
    [Route(ControllerConstants.HealthRoute)]
    public HealthResponse GetHealth()
    {
        Log.Information("Querying health.");
        return service.GetHealth();
    }

    // GET: api/roles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RolesResponse>>> GetAll()
    {
        var roles = await service.GetAllAsync();
        return Ok(roles);
    }

    // GET: api/roles/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RolesResponse>> GetById(Guid id)
    {
        var role = await service.GetByIdAsync(id);
        if (role == null)
            return NotFound(new { message = $"Role with id '{id}' not found." });

        return Ok(role);
    }

    // POST: api/roles
    [HttpPost]
    public async Task<ActionResult<RolesResponse>> Create(RolesCreateRequest request)
    {
        var created = await service.CreateAsync(request);
        return Created(nameof(Create), created);
        //return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/roles/{id}
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RolesResponse>> Update(Guid id, RolesUpdateRequest request)
    {
        var updated = await service.UpdateAsync(id, request);
        if (updated == 0)
            return NotFound(new { message = $"Role with id '{id}' not found." });

        return NoContent();
    }

    // DELETE: api/roles/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await service.DeleteAsync(id);
        if (deleted == 0)
            return NotFound(new { message = $"Role with id '{id}' not found." });

        return NoContent();
    }
}