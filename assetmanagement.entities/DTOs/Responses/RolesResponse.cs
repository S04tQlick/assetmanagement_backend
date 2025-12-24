namespace AssetManagement.Entities.DTOs.Responses;

public class RolesResponse
{
    public Guid Id { get; set; }
    public required string RoleName { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}