using AssetManagement.Entities.Models;

namespace AssetManagement.Entities.DTOs.Requests;

public class AssetCategoriesCreateRequest : BaseModel
{
    [Required, MaxLength(100)]
    public required string AssetCategoryName { get; set; } 

    [Required]
    public Guid AssetTypeId { get; set; }

    [Required]
    public Guid InstitutionId { get; set; }
}

public class AssetCategoriesUpdateRequest 
{ 

    [Required, MaxLength(100)]
    public required string AssetCategoryName { get; set; }

    [Required]
    public Guid AssetTypeId { get; set; }

    [Required]
    public Guid InstitutionId { get; set; } 
}