namespace AssetManagement.Entities.DTOs.Responses;

public class SubscriptionsResponse
{
    public Guid Id { get; set; }
    public string Plan { get; set; } = default!;
    public decimal Amount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool AutoRenew { get; set; }
    public bool IsActive { get; set; }
    public string InstitutionName { get; set; } = default!;
}