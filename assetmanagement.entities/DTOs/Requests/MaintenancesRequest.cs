using AssetManagement.Entities.Enums;
using AssetManagement.Entities.Models;

namespace AssetManagement.Entities.DTOs.Requests;

public class MaintenancesCreateRequest : BaseModel
{
    public Guid AssetId { get; set; } 
    public Guid VendorId { get; set; } 
    public required string Description { get; set; }
    public required string MaintenanceType { get; set; }  // e.g., "Preventive", "Corrective"
    public DateTime ScheduledDate  { get; set; }
    public DateTime? CompletedDate { get; set; } 
    public decimal Cost { get; set; }
    public MaintenanceStatusEnum Status { get; set; }
    public string? ReportedBy { get; set; }       // User who submitted the request
    public DateTime NextDueDate { get; set; }
}

public class MaintenancesUpdateRequest
{
    public Guid AssetId { get; set; } 
    public Guid VendorId { get; set; } 
    public required string Description { get; set; }
    public required string MaintenanceType { get; set; }  // e.g., "Preventive", "Corrective"
    public DateTime ScheduledDate  { get; set; }
    public DateTime? CompletedDate { get; set; } 
    public decimal Cost { get; set; }
    public MaintenanceStatusEnum Status { get; set; }
    public string? ReportedBy { get; set; }       // User who submitted the request
    public DateTime NextDueDate { get; set; }
}