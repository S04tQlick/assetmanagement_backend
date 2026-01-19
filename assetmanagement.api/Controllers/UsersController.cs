using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Services.UsersService; 
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUsersService service)
    : BaseApiController<UsersModel, UsersResponse, UsersCreateRequest, UsersUpdateRequest>(service);



// public class UsersController(IUsersService service) : ControllerBase
// {
//     [HttpGet]
//     [Route(ControllerConstants.HealthRoute)]
//     public HealthResponse GetHealth()
//     {
//         Log.Information("Querying health.");
//         return service.GetHealth();
//     }
//     
//     // GET: api/users
//     [HttpGet]
//     public async Task<ActionResult<IEnumerable<UsersResponse>>> GetAll()
//     {
//         var users = await service.GetAllAsync();
//         return Ok(users);
//     } 
//
//     // GET: api/users/{id}
//     [HttpGet("{id:guid}")]
//     public async Task<ActionResult<UsersResponse>> GetById(Guid id )
//     {
//         var user = await service.GetByIdAsync(id);
//         if (user == null)
//             return NotFound(new { message = $"User with id '{id}' not found." });
//
//         return Ok(user);
//     }
//
//     // GET: api/users/institution/{institutionId}
//     [HttpGet("institution/{institutionId:Guid}")]
//     public async Task<ActionResult<IEnumerable<UsersResponse>>> GetByInstitution(Guid institutionId)
//     {
//         var users = await service.GetByInstitutionIdAsync(institutionId);
//         return Ok(users);
//     }
//
//     // POST: api/users
//     [HttpPost]
//     public async Task<ActionResult<UsersResponse>> CreateAsync(UsersCreateRequest request )
//     {
//         var created = await service.CreateAsync(request);
//         return Created(nameof(CreateAsync), created);
//     }
//
//     // PUT: api/users/{id}
//     [HttpPut("{id:guid}")]
//     public async Task<ActionResult<UsersResponse>> UpdateAsync(Guid id, UsersUpdateRequest request )
//     {
//         var updated = await service.UpdateAsync(id, request);
//         return NoContent();
//     }
//
//     // DELETE: api/users/{id}
//     [HttpDelete("{id:guid}")]
//     public async Task<IActionResult> Delete(Guid id )
//     {
//         var deleted = await service.DeleteAsync(id);
//         return NoContent();
//     }
//     
//     
//     
//     // // GET: api/users/email/{email}
//     // [HttpGet("email/{email}")]
//     // public async Task<ActionResult<UsersResponse>> GetByEmail(string email, )
//     // {
//     //     var user = await service.GetByEmailAsync(email, cancellationToken);
//     //     if (user == null)
//     //         return NotFound(new { message = $"User with email '{email}' not found." });
//     //
//     //     return Ok(user);
//     // }
//     //
//     //
//     //
//     // // POST: api/users/login
//     // [HttpPost("login")]
//     // public async Task<ActionResult<UsersResponse>> Login([FromBody] LoginRequest request, )
//     // {
//     //     var user = await service.GetByEmailAsync(request.Email, cancellationToken);
//     //     if (user == null)
//     //         return Unauthorized(new { message = "Invalid email or password." });
//     //
//     //     var entity = await service.GetByIdAsync(user.Id, cancellationToken);
//     //     if (entity == null || !BCrypt.Net.BCrypt.Verify(request.Password, entity.PasswordHash))
//     //         return Unauthorized(new { message = "Invalid email or password." });
//     //
//     //     return Ok(user);
//     // }
//     
//     
// }