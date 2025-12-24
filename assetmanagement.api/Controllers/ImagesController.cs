using AssetManagement.API.DAL.SanityImageDirectory;
using AssetManagement.API.DAL.SanityImageDirectory.Services;
using AssetManagement.Entities.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController(ISanityImageService service) : ControllerBase
{
    [HttpPost("upload-image")]
    public async Task<IActionResult> CreateAsync([FromForm] SanityUploadRequest request)
    {
        if (request.File == null || request.File.Length == 0)
            return BadRequest(new { success = false, error = "No file provided" });

        Log.Information("Uploading image for file: {FileName}", request.File.FileName);

        var res = await service.CreateAsync(request);
        return Created(nameof(CreateAsync), res);
    }

    [HttpPost("replace/{docId}/{oldId}")]
    public async Task<IActionResult> Replace(string docId, string oldId, SanityUploadRequest request)
    {
        if (request.File == null || request.File.Length == 0)
            return BadRequest(new { success = false, error = "No file provided" });

        Log.Information("Replacing image for document {DocId}, old asset {OldId}, new file {FileName}", 
            docId, oldId, request.File.FileName);

        try
        {
            var result = await service.ReplaceImageAsync(docId, oldId, request);

            return result == null 
                ? StatusCode(500, new { success = false, error = "Image replacement failed" }) 
                : Ok(new { success = true, data = result });
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to replace image for document {DocId}", docId);
            return StatusCode(500, new { success = false, error = ex.Message });
        }
    }
}



















// [ApiController]
// [Route("api/[Controller]")]
// public class ImagesController(ISanityService service, IHttpClientFactory clientFactory) : ControllerBase
// {
//     // [HttpPost("replace/{docId}/{oldId}")]
//     // public async Task<IActionResult> Replace(string docId, string oldId, IFormFile file)
//     // {
//     //     var result = await service.ReplaceImageAsync(docId, oldId, file);
//     //     return Ok(result);
//     // }
//     
//     
//     
//     [HttpPost("upload-image")]
//     public async Task<IActionResult> CreateAsync([FromForm] SanityUploadRequest request)
//     {
//         Log.Information("Uploading image for file: {FileName}", request.File?.FileName);
//         var res = await service.CreateAsync(request);
//         return Created(nameof(CreateAsync), res);
//     }
//     
//     [HttpPut("update-image/{id}")]
//     public async Task<IActionResult> UpdateAsync(string id, [FromForm] SanityUploadRequest request)
//     {
//         if (request.File == null || request.File.Length == 0)
//         {
//             return BadRequest(new { success = false, error = "No file provided" });
//         }
//     
//         Log.Information("Updating image {ImageId} with new file: {FileName}", id, request.File.FileName);
//     
//         try
//         {
//             // Call service to upload new file to Sanity
//             var res = await service.UpdateAsync(id, request);
//     
//             if (res == null)
//             {
//                 return NotFound(new { success = false, error = $"Image with id {id} not found" });
//             }
//     
//             return Ok(new { success = true, data = res });
//         }
//         catch (Exception ex)
//         {
//             Log.Error(ex, "Failed to update image {ImageId}", id);
//             return StatusCode(500, new { success = false, error = ex.Message });
//         }
//     }
// }