namespace AssetManagement.Entities.DTOs.Responses;

public class InstitutionsResponse : BaseResponse
{ 
    public required string InstitutionName { get; set; } 
    public required string InstitutionEmail { get; set; }
    public required string InstitutionContactNumber { get; set; } 
    public string? PrimaryColor { get; set; }
    public string? SecondaryColor { get; set; }
    public string? LogoSanityId { get; set; }
    public string? LogoUrl { get; set; } 

    // // Optional nested responses
    // public IEnumerable<BranchesResponse>? Branches { get; set; }
    // public IEnumerable<AddressesResponse>? Addresses { get; set; }
    // public IEnumerable<InstitutionUsersResponse>? InstitutionUsers { get; set; }
    // public IEnumerable<SubscriptionsResponse>? Subscriptions { get; set; }
}