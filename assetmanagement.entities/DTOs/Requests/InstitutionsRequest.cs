using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;

namespace AssetManagement.Entities.DTOs.Requests;

public class InstitutionsCreateRequest : SanityBaseModel
{ 
    [MaxLength(200)]
    public required string InstitutionName { get; set; }

    [EmailAddress]
    public required string InstitutionEmail { get; set; }

    [Phone]
    public required string InstitutionContactNumber { get; set; }

    [MaxLength(7)]
    public required string PrimaryColor { get; set; }

    [MaxLength(7)]
    public required string SecondaryColor { get; set; }
    
    public List<FileUploadsResponse>? FileUploads { get; set; }
}

public class InstitutionsUpdateRequest
{
    [MaxLength(200)] 
    public required string InstitutionName { get; set; }

    [EmailAddress] 
    public required string InstitutionEmail { get; set; }

    [Phone] 
    public required string InstitutionContactNumber { get; set; }

    [MaxLength(7)] 
    public required string PrimaryColor { get; set; }

    [MaxLength(7)] 
    public required string SecondaryColor { get; set; }
    
    public List<FileUploadsResponse>? FileUploads { get; set; }
}


    
    // public required string LogoSanityId { get; set; }
    // public required string LogoUrl { get; set; }