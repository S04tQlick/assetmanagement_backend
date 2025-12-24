using AssetManagement.Entities.Enums;

namespace AssetManagement.Entities.DTOs.Responses;

public abstract class DepreciationReportsResponse : BaseResponse
{
    public Guid AssetId { get; set; }
    public string AssetName { get; set; } = string.Empty;
    public string InstitutionName { get; set; } = string.Empty;
    public string? BranchName { get; set; }
    public string? CategoryName { get; set; }

    public DepreciationMethodEnum MethodEnum { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal SalvageValue { get; set; }
    public int UsefulLifeYears { get; set; }

    public decimal AnnualDepreciation { get; set; }
    public decimal AccumulatedDepreciation { get; set; }
    public decimal CurrentValue { get; set; }

    public DateTime PurchaseDate { get; set; }
    public DateTime? NextMaintenanceDate { get; set; }
}