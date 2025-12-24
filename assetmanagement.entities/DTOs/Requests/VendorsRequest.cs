using AssetManagement.Entities.Models;

namespace AssetManagement.Entities.DTOs.Requests;

public class VendorsCreateRequest : BaseModel
{
    [Required, MaxLength(200)]
    public string VendorsName { get; set; } = string.Empty;

    [EmailAddress]
    public required string EmailAddress { get; set; }

    public required string ContactInfo { get; set; }

    [Required]
    public Guid InstitutionId { get; set; }
}

public class VendorsUpdateRequest
{
    [MaxLength(200)]
    public required string VendorsName { get; set; }

    [EmailAddress]
    public required string EmailAddress { get; set; }

    public required string ContactInfo { get; set; }

    public Guid InstitutionId { get; set; }
}