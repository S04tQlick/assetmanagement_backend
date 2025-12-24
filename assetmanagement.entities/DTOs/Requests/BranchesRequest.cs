using AssetManagement.Entities.Models;

namespace AssetManagement.Entities.DTOs.Requests;

public class BranchesCreateRequest : GeoBaseModel
{
    public Guid InstitutionId { get; set; }
    public required string BranchName { get; set; }

    public bool IsHeadOffice { get; set; }
}

public class BranchesUpdateRequest
{
    [Required]
    public Guid InstitutionId { get; set; }

    [Required, MaxLength(200)]
    public required string BranchName { get; set; } 
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsHeadOffice { get; set; }
}