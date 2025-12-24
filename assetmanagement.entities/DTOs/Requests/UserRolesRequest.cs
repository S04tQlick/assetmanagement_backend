using AssetManagement.Entities.Models;

namespace AssetManagement.Entities.DTOs.Requests;

public class UserRolesCreateRequest : BaseModel
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid RoleId { get; set; }
}


public class UserRolesUpdateRequest
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid RoleId { get; set; }
}