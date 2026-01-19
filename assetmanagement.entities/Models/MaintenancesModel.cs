using AssetManagement.Entities.Enums;
using AssetManagement.Entities.GeneralResponse;

namespace AssetManagement.Entities.Models;

[Table("Maintenances")]
public class MaintenancesModel : BaseModel, IInstitutionOwned
{
    [Required] [ForeignKey(nameof(Asset))] 
    public Guid AssetId { get; init; }

    [ForeignKey(nameof(Vendor))] 
    public Guid? VendorId { get; init; }

    [Required]
    [ForeignKey(nameof(Institutions))]
    public Guid InstitutionId { get; init; }
    
    [ForeignKey(nameof (InstitutionId))]
    public InstitutionsModel? Institutions { get; set; }

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
}
