namespace AssetManagement.Entities.DTOs.Responses;

public class UserRolesResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    public UsersResponse? Users { get; set; }
    public RolesResponse? Roles { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}