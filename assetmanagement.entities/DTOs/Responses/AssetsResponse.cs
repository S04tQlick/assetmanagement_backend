using AssetManagement.Entities.Enums;

namespace AssetManagement.Entities.DTOs.Responses;

public class AssetsResponse : BaseResponse
{
    public required string AssetName { get; set; } 
    public required string SerialNumber { get; set; }

    public Guid InstitutionId { get; set; } 
    public InstitutionsResponse? Institutions { get; set; }
    public Guid BranchId { get; set; } 
    public BranchesResponse?  Branches { get; set; } 
    public Guid AssetCategoryId { get; set; } 
    public AssetCategoriesResponse? AssetCategories { get; set; } 
    public Guid AssetTypeId { get; set; } 
    public AssetTypesResponse? AssetTypes { get; set; } 
    public Guid VendorId { get; set; } 
    public VendorsResponse? Vendors { get; set; } 

    public DateTime PurchaseDate { get; set; }
    public decimal PurchasePrice { get; set; }
    public int UsefulLifeYears { get; set; }

    public int UnitsTotal { get; set; }
    public int CurrentUnits { get; set; }

    public required string SanityAssetId { get; set; }
    public required string SanityUrl { get; set; }

    public DateTime MaintenanceDueDate { get; set; }
    public DateTime NextMaintenanceDate { get; set; }

    public decimal SalvageValue { get; set; }
    public required string DepreciationMethod { get; set; }

    public decimal CurrentValue { get; set; }
    public decimal AccumulatedDepreciation { get; set; }

    //public IEnumerable<MaintenanceLogsResponse>? MaintenanceLogs { get; set; } 
}