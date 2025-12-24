using AssetManagement.Entities.Enums;

namespace AssetManagement.Entities.Models;

[Table("Maintenances")]
public class MaintenancesModel : BaseModel
{
    [Required] [ForeignKey(nameof(Asset))] 
    public Guid AssetId { get; init; }

    [ForeignKey(nameof(Vendor))] 
    public Guid? VendorId { get; init; }

    [Required]
    [ForeignKey(nameof(Institution))]
    public Guid InstitutionId { get; init; }

    [Required, MaxLength(200)]
    public string Title { get; init; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; init; }

    [Required]
    public DateTime ScheduledDate { get; init; }

    public DateTime? CompletedDate { get; init; }

    public DateTime? NextDueDate { get; init; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Cost { get; init; }

    [Required, MaxLength(50)] 
    public MaintenanceStatusEnum Status { get; set; }

    public bool IsRecurring { get; init; }

    public int? RecurrenceIntervalDays { get; init; }

    public string? SanityId { get; init; }

    // Navigation Properties
    public AssetsModel? Asset { get; init; }
    public VendorsModel? Vendor { get; init; }
    public InstitutionsModel? Institution { get; init; }
}
