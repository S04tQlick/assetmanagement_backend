using Microsoft.AspNetCore.Http;

namespace AssetManagement.Entities.DTOs.Requests;

public class SanityUploadRequest
{
    public required IFormFile File  { get; set; }
}