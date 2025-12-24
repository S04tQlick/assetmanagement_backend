namespace AssetManagement.Entities.DTOs.Responses;

public class VendorsResponse
{
    public Guid Id { get; set; }
    public required string VendorsName { get; set; } 

    [EmailAddress]
    public required string EmailAddress { get; set; }

    public required string ContactInfo { get; set; }

    public Guid InstitutionId { get; set; }
    public InstitutionsResponse? Institutions { get; set; } 

    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}