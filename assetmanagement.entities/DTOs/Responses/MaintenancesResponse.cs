using AssetManagement.Entities.Enums;

namespace AssetManagement.Entities.DTOs.Responses;

public class MaintenancesResponse : BaseResponse
{ 
    public Guid AssetId { get; set; }
    public required string AssetName { get; set; } 
    public Guid? VendorId { get; set; }
    public string? VendorName { get; set; }
    public string? Description { get; set; }
    public string? MaintenanceType { get; set; }
    public DateTime ScheduledDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public decimal? Cost { get; set; }
    public MaintenanceStatusEnum Status { get; set; }
    public string? ReportedBy { get; set; }
    public DateTime NextDueDate { get; set; } 
}