using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Services.DepreciationService;

public interface IDepreciationService
{
    decimal CalculateAnnualDepreciation(AssetsModel assetsModel);
    decimal CalculateCurrentValue(AssetsModel assetsModel);
    Task UpdateDepreciationValuesAsync(AssetsModel assetsModel);
}