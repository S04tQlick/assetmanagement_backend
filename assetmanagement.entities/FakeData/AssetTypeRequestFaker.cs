using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.Enums;
using Bogus;

namespace AssetManagement.Entities.FakeData;

public static class AssetTypeRequestFaker
{
    public static Faker<AssetTypesCreateRequest> GetCreateRequestFaker()
    {
        return new Faker<AssetTypesCreateRequest>()
            .RuleFor(r => r.Id, _ => Guid.NewGuid())
            .RuleFor(r => r.AssetTypeName, f => f.Commerce.ProductMaterial())
            .RuleFor(r => r.AssetTypeName, f => f.PickRandom(Enum.GetNames<AssetTypeEnum>()))
            .RuleFor(r => r.Description, f => f.Lorem.Sentence())
            .RuleFor(r => r.CreatedAt, _ => DateTime.UtcNow)
            .RuleFor(r => r.UpdatedAt, _ => DateTime.UtcNow)
            .RuleFor(r => r.IsActive, _ => true);
    }
}