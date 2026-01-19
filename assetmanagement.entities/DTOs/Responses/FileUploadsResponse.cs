namespace AssetManagement.Entities.DTOs.Responses;

public class FileUploadsResponse : BaseResponse
{
    public string? S3Key { get; set; }
    public Guid InstitutionId { get; set; }
    public bool IsLogo { get; set; }
}