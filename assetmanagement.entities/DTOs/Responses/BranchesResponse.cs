namespace AssetManagement.Entities.DTOs.Responses;

public class BranchesResponse : BaseResponse
{ 
    public required string BranchName { get; set; }
    public Guid InstitutionId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    public bool IsHeadOffice { get; set; } 
    //public InstitutionsResponse? Institutions { get; set; }
}