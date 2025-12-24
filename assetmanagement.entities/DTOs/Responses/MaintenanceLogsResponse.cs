namespace AssetManagement.Entities.DTOs.Responses;

public class MaintenanceLogsResponse : BaseResponse
{ 
    public Guid AssetId { get; set; }
    public string? AssetName { get; set; }

    public Guid InstitutionId { get; set; }
    public string? InstitutionName { get; set; }

    public decimal Cost { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string PerformedBy { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime NextDueDate { get; set; } 
}