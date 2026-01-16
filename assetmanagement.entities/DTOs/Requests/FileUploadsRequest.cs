using Microsoft.AspNetCore.Http;

namespace AssetManagement.Entities.DTOs.Requests;

public class FileUploadsCreateRequest
{
    public required IFormFile File { get; init; } = null!;
    public string? S3Key { get; set; } 
    public Guid InstitutionId { get; set; }
    public bool IsActive { get; set; }
}

public class FileUploadsUpdateRequest
{ 
    public required IFormFile File { get; init; } = null!;
    public string? S3Key { get; set; } 
    public Guid InstitutionId { get; set; }
    public bool IsActive { get; set; }
}