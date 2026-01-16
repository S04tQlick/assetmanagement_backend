// using AssetManagement.API.DAL.Services.AwsService;
// using Microsoft.AspNetCore.Mvc;
//
// namespace AssetManagement.API.Controllers;
//
// [ApiController]
// [Route("api/s3")]
// public class S3Controller(IS3Service s3Service ) : ControllerBase
// {
//     [HttpGet]
//     public async Task<IActionResult> GetAll()
//     {
//         var result = await s3Service.GetAllAsync();
//         return Ok(result.S3Objects.Select(o => o.Key));
//     }
//     
//     [HttpPost]
//     public async Task<IActionResult> CreateAsync(IFormFile? file)
//     {
//         if (file == null || file.Length == 0)
//             return BadRequest("File missing");
//
//         await s3Service.CreateAsync(file);
//         return Ok("Uploaded");
//     }
//     
//     //[HttpGet("preview/{key}")]
//     [HttpGet("{id}")]
//     public async Task<IActionResult> GetById(string id)
//     {
//         var response = await s3Service.GetByIdAsync(id);
//         return File(response.ResponseStream, response.Headers.ContentType);
//     }
//     
//     //[HttpPut("update/{key}")]
//     [HttpPut("{id}")]
//     public async Task<IActionResult> Update(string key, IFormFile file)
//     {
//         await s3Service.UpdateAsync(key,file);
//         return Ok("Updated");
//     }
//     
//     [HttpDelete("delete/{key}")]
//     public async Task<IActionResult> Delete(string key)
//     {
//         await s3Service.DeleteAsync(key);
//         return Ok("Deleted");
//     }
// }