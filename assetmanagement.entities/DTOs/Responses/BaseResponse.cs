namespace AssetManagement.Entities.DTOs.Responses;

public abstract class BaseResponse
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}