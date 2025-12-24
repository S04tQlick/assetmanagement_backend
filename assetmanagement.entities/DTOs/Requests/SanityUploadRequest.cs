using Microsoft.AspNetCore.Http;

namespace AssetManagement.Entities.DTOs.Requests;

public class SanityUploadRequest
{
    public IFormFile? File  { get; set; }
}