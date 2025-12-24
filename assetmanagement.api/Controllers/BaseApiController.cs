using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController<TEntity, TResponse, TCreateRequest, TUpdateRequest>(IServiceQueryHandler<TEntity, TResponse, TCreateRequest, TUpdateRequest> service ) : ControllerBase where TEntity : BaseModel
{
    [HttpGet]
    public virtual async Task<IEnumerable<TResponse>> GetAll()
    {
        Log.Information("Querying {Entity} service", typeof(TEntity).Name);
        return await service.GetAllAsync();
    }

    [HttpGet("{id:guid}")]
    public virtual async Task<ActionResult<TResponse?>> GetById(Guid id)
    {
        Log.Information("Fetching {Entity} by id {Id}", typeof(TEntity).Name, id);
        return await service.GetByIdAsync(id);
    }

    [HttpPost]
    public virtual async Task<ActionResult<TResponse>> CreateAsync( TCreateRequest request)
    {
        Log.Information("{Entity} added to database.", typeof(TEntity).Name);
        var res = await service.CreateAsync(request); 
        return Created(nameof(CreateAsync), res);
    }

    [HttpPut("{id:guid}")]
    public virtual async Task<IActionResult> UpdateAsync(Guid id, TUpdateRequest request)
    {
        Log.Information("{Entity} updated in database.", typeof(TEntity).Name);
        await service.UpdateAsync( id,request);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
    {
        Log.Information("{Entity} deleted from database.", typeof(TEntity).Name);
        var deleted = await service.DeleteAsync(id);
        if (deleted == 0) return NotFound();
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
//         return await service.GetAllAsync(); 
//     }
//
//     [HttpGet("{id:guid}")]
//     public virtual async Task<ActionResult<TResponse>> GetById(Guid id)
//     {
//         Log.Information("Fetching {Entity} by id {Id}", typeof(TEntity).Name, id);
//        return  (await service.GetByIdAsync(id))!;
//     }
//
//     [HttpPost]
//     public virtual async Task<ActionResult<TResponse>> CreateAsync([FromBody] TCreateRequest request)
//     {
//         var res = await service.CreateAsync(request);
//         return Created(nameof(CreateAsync), res);
//     }
//
//     [HttpPut("{id:guid}")]
//     public virtual async Task<ActionResult<TResponse>> Update(Guid id, [FromBody] TUpdateRequest request)
//     {
//         await service.UpdateAsync(id,request);
//         return NoContent();
//     }
//
//     [HttpDelete("{id:guid}")]
//     public virtual async Task<IActionResult> Delete(Guid id)
//     {
//         await service.DeleteAsync(id);
//         return NoContent();
//     }
// }