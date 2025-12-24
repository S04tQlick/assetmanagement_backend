using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Enums;
using Bogus;

namespace AssetManagement.Entities.FakeData;

public static class AssetCategoriesRequestFaker
{
    public static Faker<AssetCategoriesCreateRequest> GetCreateRequestFaker(IEnumerable<AssetTypesResponse> seededTypes,
        Guid institutionId)
    {
        return new Faker<AssetCategoriesCreateRequest>()
            .RuleFor(r => r.Id, _ => Guid.NewGuid())
            .RuleFor(r => r.AssetTypeId, f =>
            {
                var type = f.PickRandom(seededTypes);
                return type.Id;
            })
            .RuleFor(r => r.AssetCategoryName, (f, r) =>
            {
                var typeEnum = Enum.Parse<AssetTypeEnum>(
                    seededTypes.First(t => t.Id == r.AssetTypeId).AssetTypeName);

                if (AssetCategoryMap.Categories.TryGetValue(typeEnum, out var categories))
                    return f.PickRandom(categories);

                throw new InvalidOperationException(
                    $"No categories defined for asset type {typeEnum}");
            })
            .RuleFor(r => r.InstitutionId, _ => institutionId)
            .RuleFor(r => r.CreatedAt, _ => DateTime.UtcNow)
            .RuleFor(r => r.UpdatedAt, _ => DateTime.UtcNow)
            .RuleFor(r => r.IsActive, _ => true);
    }
}