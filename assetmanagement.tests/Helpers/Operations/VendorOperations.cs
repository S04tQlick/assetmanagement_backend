using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers.ApiOperations;
using SixLabors.Fonts;

namespace AssetManagement.Tests.Helpers.Operations;

public class VendorOperations (ApplicationFixture  fixture)
{
    protected async Task<IEnumerable<VendorsResponse>?> GetVendorsDataAsync()
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetVendorsControllerRoute());
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<VendorsResponse>>(content);
    }

    public async Task<IEnumerable<VendorsResponse>?> GetVendorsByInstitutionIdAsync(string insitutionId)
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetVendorsControllerRoute($"{insitutionId}"));
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<VendorsResponse>>(content);
    }
    
    protected async Task<bool> VendorExistsByNameAndInstitutionIdAsync(string name, Guid institutionId)
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetAssetCategoriesControllerRoute($"institution/{institutionId}"));
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var assetCategories = TestOperations.Deserialize<IEnumerable<AssetCategoriesResponse>>(content);

        return assetCategories?.Any(a => 
            string.Equals(a.AssetCategoryName.Trim().ToLower(), name.Trim().ToLower(), StringComparison.OrdinalIgnoreCase)
        ) ?? false;
    }
}