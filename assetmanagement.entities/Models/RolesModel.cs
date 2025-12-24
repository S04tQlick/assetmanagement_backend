namespace AssetManagement.Entities.Models;

[Table("Roles")]
public class RolesModel : BaseModel
{
    public required string RoleName { get; set; }

    public ICollection<UserRolesModel>? UserRoles { get; set; }
}