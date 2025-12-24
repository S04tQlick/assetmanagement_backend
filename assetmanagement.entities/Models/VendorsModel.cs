namespace AssetManagement.Entities.Models;

[Table("Vendors")]
public class VendorsModel : BaseModel
{
    [Required, MaxLength(200)]
    public required string VendorsName { get; set; }

    [EmailAddress]
    public required string EmailAddress { get; set; }

    public required string ContactInfo { get; set; }
    
    public Guid InstitutionId { get; set; }
    [ForeignKey(nameof (InstitutionId))]
    public InstitutionsModel? Institutions { get; set; }
    

    public ICollection<AssetsModel>? Assets { get; set; }
}