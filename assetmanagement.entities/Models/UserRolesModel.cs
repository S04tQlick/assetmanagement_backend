namespace AssetManagement.Entities.Models;

[Table("UserRoles")]
public class UserRolesModel : BaseModel
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public UsersModel? Users { get; set; }
    
    [ForeignKey(nameof(RoleId))]
    public RolesModel? Roles { get; set; }
}