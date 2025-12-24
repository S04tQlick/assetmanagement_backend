using AssetManagement.Entities.Models;

namespace AssetManagement.Entities.DTOs.Requests;

public class AssetsCreateRequest : BaseModel
{
    [Required, MaxLength(200)]
    public string AssetName { get; set; } = string.Empty;
    
    [Required]
    public Guid InstitutionId { get; set; }

    [Required]
    public Guid BranchId { get; set; }

    [Required]
    public Guid AssetCategoryId { get; set; }

    [Required]
    public Guid AssetTypeId { get; set; }

    [Required]
    public Guid VendorId { get; set; }

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
}

public class AssetsUpdateRequest  
{ 
    [Required]
    public Guid InstitutionId { get; set; }

    [Required]
    public Guid BranchId { get; set; }

    [Required]
    public Guid AssetCategoryId { get; set; }

    [Required]
    public Guid AssetTypeId { get; set; }

    [Required]
    public Guid VendorId { get; set; }

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
}