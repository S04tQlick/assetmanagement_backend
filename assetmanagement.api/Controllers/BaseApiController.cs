using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.API.Controllers;

public record EndpointConfig( bool GetAll = true, bool GetById = true, bool GetByInstitutionId = true, bool GetActive = true, bool GetInactive = true, bool Create = true, bool Update = true, bool Delete = true );


[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController<TEntity, TResponse, TCreateRequest, TUpdateRequest>(IServiceQueryHandler<TEntity, TResponse, TCreateRequest, TUpdateRequest> service) : ControllerBase where TEntity : BaseModel
{
    protected virtual EndpointConfig Config => new();
   
    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = false)]
    public virtual async Task<ActionResult<IEnumerable<TResponse>>> GetAll()
    {
        if (!Config.GetAll) return NotFound();
        var results = await service.GetAllAsync();
        return Ok(results);
    }

    [HttpGet("{id:guid}")] 
    [ApiExplorerSettings(IgnoreApi = false)]
    public virtual async Task<ActionResult<TResponse>> GetById(Guid id)
    {
        if (!Config.GetById) return NotFound();
        var result = await service.GetByIdAsync(id);
        return Ok(result);
    }

    [ApiExplorerSettings(IgnoreApi = false)]
    [HttpGet("institution/{institutionId}")]
    public virtual async Task<ActionResult<IEnumerable<TResponse>>> GetByInstitutionId(Guid institutionId)
    {
        if (!Config.GetByInstitutionId) return NotFound();
        var results = await service.GetByInstitutionIdAsync(institutionId);
        return Ok(results);
    }

    [HttpGet("active")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public virtual async Task<ActionResult<IEnumerable<TResponse>>> GetActiveAsync()
    {
        if (!Config.GetActive) return NotFound();
        var results = await service.GetAllAsync();
        return Ok(results);
    }

    [HttpGet("inactive")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public virtual async Task<ActionResult<IEnumerable<TResponse>>> GetNotActiveAsync()
    {
        if (!Config.GetInactive) return NotFound();
        var results = await service.IsNotActiveAsync();
        return Ok(results);
    }

    [HttpPost]
    [ApiExplorerSettings(IgnoreApi = false)]
    public virtual async Task<ActionResult> CreateAsync(TCreateRequest request)
    {
        if (!Config.Create) return NotFound();
        var created = await service.CreateAsync(request);
        return Created(nameof(CreateAsync), created);
    }

    [HttpPut("{id:guid}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public virtual async Task<IActionResult> UpdateAsync(Guid id, TUpdateRequest request)
    {
        if (!Config.Update) return NotFound();
        await service.UpdateAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
    {
        if (!Config.Delete) return NotFound();
        await service.DeleteAsync(id);
        return NoContent();
    }
}






















// [ApiController]
// [Route("api/[controller]")]
// public abstract class BaseApiController<TEntity, TResponse, TCreateRequest, TUpdateRequest>(IServiceQueryHandler<TEntity, TResponse, TCreateRequest, TUpdateRequest> service) : ControllerBase where TEntity : BaseModel
// {
//     [HttpGet]
//     public virtual async Task<ActionResult<IEnumerable<TResponse>>> GetAll()
//     {
//         Log.Information("Querying all {Entity}", typeof(TEntity).Name);
//
//         var results = await service.GetAllAsync();
//         
//         return Ok(results);
//     }
//     
//     [HttpGet("{id:guid}")]
//     public virtual async Task<ActionResult<TResponse>> GetById(Guid id)
//     {
//         Log.Information("Fetching {Entity} by id {Id}", typeof(TEntity).Name, id);
//
//         var result = await service.GetByIdAsync(id);
//
//         return Ok(result);
//     }
//     
//     [HttpGet("institution/{institutionId}")]
//     public virtual async Task<ActionResult<IEnumerable<TResponse>>> GetByInstitutionId(Guid institutionId)
//     {
//         var assets = await service.GetByInstitutionIdAsync(institutionId);
//         return Ok(assets);
//     }
//     
//     [HttpGet("active")]
//     public virtual async Task<ActionResult<IEnumerable<TResponse>>> GetActiveAsync()
//     {
//         Log.Information("Fetching {Entity} is Active ", typeof(TEntity).Name);
//
//         var results = await service.GetAllAsync();
//         
//         return Ok(results);
//     }
//     
//     [HttpGet("inactive")]
//     public virtual async Task<ActionResult<IEnumerable<TResponse>>> GetNotActiveAsync()
//     {
//         Log.Information("Fetching {Entity} is Not Active", typeof(TEntity).Name);
//
//         var results = await service.IsNotActiveAsync();
//         
//         return Ok(results);
//     }  
//
//     [HttpPost]
//     public virtual async Task<ActionResult> CreateAsync(TCreateRequest request)
//     {
//         Log.Information("Creating new {Entity}", typeof(TEntity).Name);
//
//         var created = await service.CreateAsync(request);
//         
//         Log.Information("{Entity} Created successfully", typeof(TEntity).Name); 
//
//         return Created(nameof(CreateAsync), created);
//     }
//
//     [HttpPut("{id:guid}")]
//     public virtual  async Task<IActionResult> UpdateAsync(Guid id, TUpdateRequest request)
//     {
//         Log.Information("Updating {Entity} with id {Id}", typeof(TEntity).Name, id);
//
//         await service.UpdateAsync(id, request);
//         
//         Log.Information("{Entity} with id {Id} Updated successfully", typeof(TEntity).Name, id); 
//
//         return NoContent();
//     }
//
//     [HttpDelete("{id:guid}")]
//     public async Task<IActionResult> DeleteAsync(Guid id)
//     {
//         Log.Information("Deleting {Entity} with id {Id}", typeof(TEntity).Name, id); 
//         
//         await service.DeleteAsync(id); 
//         
//         Log.Information("{Entity} with id {Id} deleted successfully", typeof(TEntity).Name, id); 
//         
//         return NoContent();
//     }
// }