namespace AssetManagement.Entities.Models;

[Table("Subscriptions")]
public class SubscriptionsModel : BaseModel
{
    [Required]
    [ForeignKey(nameof(Institution))]
    public Guid InstitutionId { get; set; }

    public string? SubscriptionPlan { get; set; }
    
    [Required]
    public DateTime SubscriptionStartDate { get; set; }

    [Required]
    public DateTime SubscriptionEndDate { get; set; }
    
    public int DurationInMonths { get; set; }
    
    public DateTime? NextReminderDate { get; set; }
    
    public DateTime? LastRenewedAt { get; set; }
    
    [MaxLength(500)]
    public string? Notes { get; set; }
    public InstitutionsModel? Institution { get; set; }
}