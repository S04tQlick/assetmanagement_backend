namespace AssetManagement.Entities.Models;

[Table("AssetCategories")]
public class AssetCategoriesModel : BaseModel
{
    [MaxLength(100)]
    public required string AssetCategoryName { get; set; }

    public Guid AssetTypeId { get; set; }
    [ForeignKey(nameof(AssetTypeId))]
    public AssetTypesModel? AssetTypes { get; set; }
    
    public Guid InstitutionId { get; set; }
    [ForeignKey(nameof(InstitutionId))]
    public InstitutionsModel? Institutions{ get; set; }
}