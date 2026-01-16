using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Services.RolesService;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController(IRoleService service)
    : BaseApiController<RolesModel, RolesResponse, RolesCreateRequest, RolesUpdateRequest>(service)
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<IEnumerable<RolesResponse>>> GetByInstitutionId(Guid institutionId) =>
        Task.FromResult<ActionResult<IEnumerable<RolesResponse>>>(NotFound());
} 