namespace AssetManagement.Entities.Models;

[Table("AssetTypes")]
public class AssetTypesModel : BaseModel
{
    [MaxLength(100)]
    public required string AssetTypeName { get; set; }

    [MaxLength(500)]
    public required string Description { get; set; }

    public ICollection<AssetCategoriesModel>? Categories { get; set; }
    public ICollection<AssetsModel>? Assets { get; set; }
}