using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.Enums;
using Bogus;

namespace AssetManagement.Entities.FakeData;

public abstract class AssetRequestFaker
{
    public static Faker<AssetsCreateRequest> GetCreateRequestFaker(
        Guid institutionId, Guid branchId, Guid assetTypeId, Guid assetCategoryId, Guid vendorId)
    {
        return new Faker<AssetsCreateRequest>()
            .RuleFor(a => a.Id, _ => Guid.NewGuid())
            .RuleFor(a => a.AssetName, f => f.Commerce.ProductName())
            .RuleFor(a => a.SerialNumber, f => f.Random.AlphaNumeric(10).ToUpper())
            .RuleFor(a => a.InstitutionId, _ => institutionId)
            .RuleFor(a => a.BranchId, _ => branchId)
            .RuleFor(a => a.AssetCategoryId, _ => assetCategoryId)
            .RuleFor(a => a.AssetTypeId, _ => assetTypeId)
            .RuleFor(a => a.VendorId, _ => vendorId)
            .RuleFor(a => a.PurchaseDate, f => f.Date.Past(5, DateTime.UtcNow))
            .RuleFor(a => a.PurchasePrice, f => f.Finance.Amount(100, 5000))
            .RuleFor(a => a.UsefulLifeYears, f => f.Random.Int(3, 10))
            .RuleFor(a => a.UnitsTotal, f => f.Random.Int(1, 50))
            .RuleFor(a => a.CurrentUnits, (f, a) => f.Random.Int(0, a.UnitsTotal))
            .RuleFor(a => a.SanityAssetId, f => f.Random.Guid().ToString())
            .RuleFor(a => a.SanityUrl, f => f.Internet.Url())
            .RuleFor(a => a.MaintenanceDueDate, f => f.Date.Future(1,DateTime.UtcNow))
            .RuleFor(a => a.NextMaintenanceDate, f => f.Date.Future(1,DateTime.UtcNow))
            .RuleFor(a => a.SalvageValue, f => f.Finance.Amount(10, 500))
            .RuleFor(a => a.DepreciationMethod, f => f.PickRandom(Enum.GetNames<DepreciationMethodEnum>()))
            .RuleFor(a => a.CurrentValue, f => f.Finance.Amount(100, 5000))
            .RuleFor(a => a.AccumulatedDepreciation, f => f.Finance.Amount(100, 5000))
            .RuleFor(a => a.IsActive, _ => true)
            .RuleFor(a => a.CreatedAt, _ => DateTime.UtcNow)
            .RuleFor(a => a.UpdatedAt, _ => DateTime.UtcNow);
    }
}