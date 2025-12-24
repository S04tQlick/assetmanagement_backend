using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers.ApiOperations;

namespace AssetManagement.Tests.Helpers.Operations;

public class AssetCategoriesOperations (ApplicationFixture  fixture)
{
    protected async Task<bool> AssetCategoriesExistsByNameAndInstitutionIdAsync(string name, Guid institutionId, Guid assetTypeId)
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetAssetCategoriesControllerRoute($"institution/{institutionId}/type/{assetTypeId}"));
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var assetCategories = TestOperations.Deserialize<IEnumerable<AssetCategoriesResponse>>(content);

        return assetCategories?.Any(a =>
            string.Equals(a.AssetCategoryName.Trim(), name.Trim(), StringComparison.OrdinalIgnoreCase)
        ) ?? false;
    }
}