using AssetManagement.Entities.Models;

namespace AssetManagement.Entities.DTOs.Responses;

public class AssetCategoriesResponse
{
    public Guid Id { get; init; }

    public required string AssetCategoryName { get; init; }

    public Guid AssetTypeId { get; init; } 
    public AssetTypesResponse? AssetTypes { get; set; }

    public Guid InstitutionId { get; init; }
    public InstitutionsResponse? Institutions { get; set; } 
    public bool IsActive { get; init; }

    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}