using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController<TEntity, TResponse, TCreateRequest, TUpdateRequest>(IServiceQueryHandler<TEntity, TResponse, TCreateRequest, TUpdateRequest> service) : ControllerBase where TEntity : BaseModel
{
    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TResponse>>> GetAll()
    {
        Log.Information("Querying all {Entity}", typeof(TEntity).Name);

        var results = await service.GetAllAsync();
        return Ok(results);
    }

    [HttpGet("{id:guid}")]
    public virtual async Task<ActionResult<TResponse>> GetById(Guid id)
    {
        Log.Information("Fetching {Entity} by id {Id}", typeof(TEntity).Name, id);

        var result = await service.GetByIdAsync(id);

        if (result is null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = $"{typeof(TEntity).Name} not found",
                Detail = $"No {typeof(TEntity).Name} exists with id {id}"
            });
        }

        return Ok(result);
    }

    [HttpPost]
    public virtual async Task<ActionResult<TResponse>> CreateAsync(TCreateRequest request)
    {
        Log.Information("Creating new {Entity}", typeof(TEntity).Name);

        var created = await service.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = created }, created);
    }

    [HttpPut("{id:guid}")]
    public virtual async Task<IActionResult> UpdateAsync(Guid id, TUpdateRequest request)
    {
        Log.Information("Updating {Entity} with id {Id}", typeof(TEntity).Name, id);

        var updated = await service.UpdateAsync(id, request);

        if (updated == 0)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = $"{typeof(TEntity).Name} not found",
                Detail = $"No {typeof(TEntity).Name} exists with id {id}"
            });
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
    {
        Log.Information("Deleting {Entity} with id {Id}", typeof(TEntity).Name, id);

        var deleted = await service.DeleteAsync(id);

        if (deleted == 0)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = $"{typeof(TEntity).Name} not found",
                Detail = $"No {typeof(TEntity).Name} exists with id {id}"
            });
        }

        return NoContent();
    }
}




// using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
// using AssetManagement.Entities.Models;
// using Microsoft.AspNetCore.Mvc;
// using Serilog;
//
// namespace AssetManagement.API.Controllers;
//
// [ApiController]
// [Route("api/[controller]")]
// public abstract class BaseApiController<TEntity, TResponse, TCreateRequest, TUpdateRequest>(IServiceQueryHandler<TEntity, TResponse, TCreateRequest, TUpdateRequest> service ) : ControllerBase where TEntity : BaseModel
// {
//     [HttpGet]
//     public virtual async Task<IEnumerable<TResponse>> GetAll()
//     {
//         Log.Information("Querying {Entity} service", typeof(TEntity).Name);
//         return await service.GetAllAsync();
//     }
//
//     [HttpGet("{id:guid}")]
//     public virtual async Task<ActionResult<TResponse?>> GetById(Guid id)
//     {
//         Log.Information("Fetching {Entity} by id {Id}", typeof(TEntity).Name, id);
//         return await service.GetByIdAsync(id);
//     }
//
//     [HttpPost]
//     public virtual async Task<ActionResult<TResponse>> CreateAsync( TCreateRequest request)
//     {
//         Log.Information("{Entity} added to database.", typeof(TEntity).Name);
//         var res = await service.CreateAsync(request); 
//         return Created(nameof(CreateAsync), res);
//     }
//
//     [HttpPut("{id:guid}")]
//     public virtual async Task<IActionResult> UpdateAsync(Guid id, TUpdateRequest request)
//     {
//         Log.Information("{Entity} updated in database.", typeof(TEntity).Name);
//         await service.UpdateAsync( id,request);
//         return NoContent();
//     }
//
//     [HttpDelete("{id:guid}")]
//     public virtual async Task<IActionResult> DeleteAsync(Guid id)
//     {
//         Log.Information("{Entity} deleted from database.", typeof(TEntity).Name);
//         var deleted = await service.DeleteAsync(id);
//         if (deleted == 0) return NotFound();
//         return NoContent();
//     }
// }