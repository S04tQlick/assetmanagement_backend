namespace AssetManagement.Entities.Models;

[Table("Institutions")]
public class InstitutionsModel : SanityBaseModel
{ 
    [Required, MaxLength(200)]
    public required string InstitutionName { get; set; }
    public required string InstitutionEmail { get; set; }
    
    public required string InstitutionContactNumber { get; set; }

    [MaxLength(7)]
    public string? PrimaryColor { get; set; }

    [MaxLength(7)]
    public string? SecondaryColor { get; set; }

     public SubscriptionsModel? Subscription { get; set; }
    public IEnumerable<FileUploadsModel>? FileUploads { get; set; }
    public IEnumerable<BranchesModel>? Branches { get; set; }
    
    
    
    // public ICollection<UsersModel>? Users { get; set; }
    // public ICollection<AssetsModel>? Assets { get; set; }
    // public ICollection<AddressesModel>? Address { get; set; }
    // public ICollection<MaintenanceLogsModel>? MaintenanceLogs { get; set; }
    
}