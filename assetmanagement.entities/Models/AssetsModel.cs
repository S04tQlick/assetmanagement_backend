using AssetManagement.Entities.GeneralResponse;

namespace AssetManagement.Entities.Models;

[Table("Assets")]
public class AssetsModel : BaseModel, IInstitutionOwned
{
    [Required]
    public Guid InstitutionId { get; set; } 
    
    [ForeignKey(nameof (InstitutionId))]
    public InstitutionsModel? Institutions { get; set; }

    [Required]
    public Guid BranchId { get; set; }

    [ForeignKey(nameof(BranchId))]
    public BranchesModel? Branches { get; set; }

    [Required]
    public Guid AssetCategoryId { get; set; }
    [ForeignKey(nameof(AssetCategoryId))]
    public AssetCategoriesModel? AssetCategories { get; set; }

    [Required]
    public Guid AssetTypeId { get; set; }
    [ForeignKey(nameof(AssetTypeId))]
    public AssetTypesModel? AssetTypes { get; set; }

    [Required]
    public Guid VendorId { get; set; }
    [ForeignKey(nameof(VendorId))]
    public VendorsModel? Vendors { get; set; }

    [Required, MaxLength(200)]
    public string AssetName { get; set; } = string.Empty;

    public required string SerialNumber { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal PurchasePrice { get; set; }

    public int UsefulLifeYears { get; set; }


    public int UnitsTotal { get; set; }
    public int CurrentUnits { get; set; }

    public string? SanityAssetId { get; set; }
    public string? SanityUrl { get; set; }

    public DateTime MaintenanceDueDate { get; set; } 
    
    public decimal SalvageValue { get; set; }
    public required string DepreciationMethod { get; set; }

    public decimal CurrentValue { get; set; }
    public decimal AccumulatedDepreciation { get; set; }

    public DateTime NextMaintenanceDate { get; set; }
    

    public ICollection<MaintenanceLogsModel>? MaintenanceLogs { get; set; }
}