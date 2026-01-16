using AssetManagement.API.DAL.Services.UserRolesService;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserRolesController(IUserRoleService service)
    : BaseApiController<UserRolesModel, UserRolesResponse, UserRolesCreateRequest, UserRolesUpdateRequest>(service)
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<IEnumerable<UserRolesResponse>>> GetByInstitutionId(Guid institutionId) =>
        Task.FromResult<ActionResult<IEnumerable<UserRolesResponse>>>(NotFound());
}





 


// public class UserUserRolesController(IUserRoleService service) : ControllerBase
// { 
//
//     // GET: api/userroles/role/{roleId}
//     [HttpGet("role/{roleId:guid}")]
//     public async Task<ActionResult<IEnumerable<UserUserRolesResponse>>> GetByRoleId(Guid roleId)
//     {
//         var userUserRoles = await service.GetByRoleIdAsync(roleId);
//         return Ok(userUserRoles);
//     }   
// }