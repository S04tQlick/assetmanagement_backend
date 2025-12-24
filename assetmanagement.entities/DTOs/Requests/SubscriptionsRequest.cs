namespace AssetManagement.Entities.DTOs.Requests;

public class SubscriptionsCreateRequest
{
    public Guid InstitutionId { get; set; }
    public string Plan { get; set; } = "Standard";
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime EndDate { get; set; }
    public decimal Amount { get; set; }
    public bool AutoRenew { get; set; } = true;
}

public class SubscriptionsUpdateRequest  
{
    public Guid Id { get; set; }
}