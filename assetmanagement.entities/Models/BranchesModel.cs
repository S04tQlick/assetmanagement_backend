using AssetManagement.Entities.GeneralResponse;

namespace AssetManagement.Entities.Models;

[Table("Branches")]
public class BranchesModel : GeoBaseModel, IInstitutionOwned
{
    [Required]
    public Guid InstitutionId { get; set; } 
    
    [ForeignKey(nameof (InstitutionId))]
    public InstitutionsModel? Institutions { get; set; }

    [Required, MaxLength(200)]
    public string BranchName { get; set; } = string.Empty;

    public bool IsHeadOffice { get; set; }

    public ICollection<AssetsModel>? Assets { get; set; }
    public ICollection<AddressesModel>? Address { get; set; }
}