using AssetManagement.Entities.Models;

namespace AssetManagement.Entities.DTOs.Requests;

public class RolesCreateRequest : BaseModel
{
    [Required, MaxLength(100)]
    public required string RoleName { get; set; }
}

public class RolesUpdateRequest 
{
    [Required, MaxLength(100)]
    public required string RoleName { get; set; }
}