using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Services.UserRolesService;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserRolesController(IUserRolesService service) : ControllerBase
{
    [HttpGet]
    [Route(ControllerConstants.HealthRoute)]
    public HealthResponse GetHealth()
    {
        Log.Information("Querying health.");
        return service.GetHealth();
    }
    
    // GET: api/userroles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserRolesResponse>>> GetAll()
    {
        var userRoles = await service.GetAllAsync();
        return Ok(userRoles);
    }

    // GET: api/userroles/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserRolesResponse>> GetById(Guid id)
    {
        var userRole = await service.GetByIdAsync(id);
        if (userRole == null)
            return NotFound(new { message = $"UserRole with id '{id}' not found." });

        return Ok(userRole);
    }

    // GET: api/userroles/user/{userId}
    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<UserRolesResponse>>> GetByUserId(Guid userId )
    {
        var userRoles = await service.GetByUserIdAsync(userId);
        return Ok(userRoles);
    }

    // GET: api/userroles/role/{roleId}
    [HttpGet("role/{roleId:guid}")]
    public async Task<ActionResult<IEnumerable<UserRolesResponse>>> GetByRoleId(Guid roleId)
    {
        var userRoles = await service.GetByRoleIdAsync(roleId);
        return Ok(userRoles);
    }

    // POST: api/userroles
    [HttpPost]
    public async Task<ActionResult<UserRolesResponse>> CreateAsync(UserRolesCreateRequest request)
    {
        var created = await service.CreateAsync(request);
        return Created(nameof(CreateAsync), created);
    }

    // PUT: api/userroles/{id}
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UserRolesResponse>> UpdateAsync(Guid id, UserRolesUpdateRequest request)
    {
        var updated = await service.UpdateAsync(id, request);
        if (updated == 0)
            return NotFound(new { message = $"UserRole with id '{id}' not found." });

        return NoContent();
    }

    // DELETE: api/userroles/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await service.DeleteAsync(id);
        if (deleted ==0)
            return NotFound(new { message = $"UserRole with id '{id}' not found." });

        return NoContent();
    }
}