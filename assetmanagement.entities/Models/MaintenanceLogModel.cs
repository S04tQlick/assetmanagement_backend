namespace AssetManagement.Entities.Models;

[Table("MaintenanceLogs")]
public class MaintenanceLogsModel : BaseModel
{
    [Required] public Guid AssetId { get; set; }

    [ForeignKey(nameof(AssetId))] 
    public AssetsModel? Asset { get; set; }

    public decimal Cost { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public required string PerformedBy { get; set; }
    public required string Description { get; set; }
    public required DateTime NextDueDate { get; set; }
    public Guid InstitutionId { get; set; }
    
    [ForeignKey(nameof(InstitutionId))]
    public InstitutionsModel? Institution { get; set; }
}