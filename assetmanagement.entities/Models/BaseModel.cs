namespace AssetManagement.Entities.Models;

public class BaseModel
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
    public bool IsActive { get; set; }
}