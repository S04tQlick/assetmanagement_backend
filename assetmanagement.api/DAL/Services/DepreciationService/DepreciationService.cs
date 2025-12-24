using AssetManagement.API.DAL.Repositories.AssetsRepository;
using AssetManagement.Entities.Enums;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Services.DepreciationService;

public class DepreciationService(IAssetRepository assetRepository, ILogger<DepreciationService> logger)
    : IDepreciationService
{
    public decimal CalculateAnnualDepreciation(AssetsModel assetsModel)
    {
        if (assetsModel.UsefulLifeYears <= 0)
            return 0;

        var cost = assetsModel.PurchasePrice;
        var salvage = assetsModel.SalvageValue;
        var life = assetsModel.UsefulLifeYears;

        switch (assetsModel.DepreciationMethod)
        {
            case "StraightLine" :
                return (cost - salvage) / life;

            case "DecliningBalance":
                var rate = 2m / life; // Double Declining Balance
                var bookValue = assetsModel.CurrentValue > 0 ? assetsModel.CurrentValue : cost;
                return bookValue * rate;

            case "SumOfYearsDigits":
                var sumOfYears = life * (life + 1) / 2m;


                var yearsUsed = (DateTime.UtcNow.Year - assetsModel.PurchaseDate.Year);
                var remainingYears = Math.Max(life - yearsUsed, 0);
                return (remainingYears / sumOfYears) * (cost - salvage);

            default:
                return 0;
        }
    }

    public decimal CalculateCurrentValue(AssetsModel assetsModel)
    {
        var yearsUsed = (DateTime.UtcNow.Year - assetsModel.PurchaseDate.Year);
        yearsUsed = Math.Min(yearsUsed, assetsModel.UsefulLifeYears);

        switch (assetsModel.DepreciationMethod)
        {
            case "StraightLine":
                var depreciation = (assetsModel.PurchasePrice - assetsModel.SalvageValue) / assetsModel.UsefulLifeYears;
                return Math.Max(assetsModel.PurchasePrice - (depreciation * yearsUsed), assetsModel.SalvageValue);

            case "DecliningBalance":
                var rate = 2m / assetsModel.UsefulLifeYears;
                var bookValue = assetsModel.PurchasePrice;
                for (var i = 0; i < yearsUsed; i++)
                {
                    bookValue -= bookValue * rate;
                }

                return Math.Max(bookValue, assetsModel.SalvageValue);

            case "SumOfYearsDigits":
                var n = assetsModel.UsefulLifeYears;
                var sumOfYears = n * (n + 1) / 2m;
                decimal totalDepreciation = 0;
                for (var i = 0; i < yearsUsed; i++)
                {
                    var remainingYears = n - i;
                    totalDepreciation += (remainingYears / sumOfYears) * (assetsModel.PurchasePrice - assetsModel.SalvageValue);
                }

                return Math.Max(assetsModel.PurchasePrice - totalDepreciation, assetsModel.SalvageValue);

            default:
                return assetsModel.PurchasePrice;
        }
    }

    public async Task UpdateDepreciationValuesAsync(AssetsModel assetsModel)
    {
        assetsModel.AccumulatedDepreciation = (decimal)(assetsModel.PurchasePrice - CalculateCurrentValue(assetsModel));
        assetsModel.CurrentValue = CalculateCurrentValue(assetsModel);

        await assetRepository.UpdateAsync(assetsModel);
        await assetRepository.SaveChangesAsync();

        logger.LogInformation("Updated depreciation for asset {AssetName}: current value {AssetCurrentValue:C}",
            assetsModel.AssetName, assetsModel.CurrentValue);
    }
}