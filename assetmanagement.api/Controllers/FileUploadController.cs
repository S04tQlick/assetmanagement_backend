using AssetManagement.API.DAL.Services.FileUploadService;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileUploadController(IFileUploadService service)
    : BaseApiController<FileUploadsModel, FileUploadsResponse, FileUploadsCreateRequest,
        FileUploadsUpdateRequest>(service)
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<IEnumerable<FileUploadsResponse>>> GetByInstitutionId(Guid institutionId) =>
        Task.FromResult<ActionResult<IEnumerable<FileUploadsResponse>>>(NotFound());
    
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<FileUploadsResponse>> GetById(Guid id) =>
        Task.FromResult<ActionResult<FileUploadsResponse>>(NotFound());

    
    [HttpGet("preview/{id:guid}")]
    public async Task<IActionResult> Preview(Guid id)
    {
        var response = await service.GetByIdAsync(id);
        return File(response.ResponseStream, response.Headers.ContentType);
    }
}