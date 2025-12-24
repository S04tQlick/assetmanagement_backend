using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers.ApiOperations;

namespace AssetManagement.Tests.Helpers.Operations;

public class AssetTypeOperations(ApplicationFixture fixture)
{
    protected async Task<IEnumerable<AssetTypesResponse>?> GetAssetTypeAsync( )
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetAssetTypesControllerRoute());
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<AssetTypesResponse>>(content);
    }
    
    protected async Task<IEnumerable<AssetTypesResponse>?> GetAssetTypeByDateAsync(string date)
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetAssetTypesControllerRoute(date));
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<AssetTypesResponse>>(content);
    }
    
    protected async Task<bool> AssetTypesExistsByNameAsync(string name)
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetAssetTypesControllerRoute());
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var assetTypes = TestOperations.Deserialize<IEnumerable<AssetTypesResponse>>(content);

        return assetTypes?.Any(a => 
            string.Equals(a.AssetTypeName?.Trim().ToLower(), name?.Trim().ToLower(), StringComparison.OrdinalIgnoreCase)
        ) ?? false;
    }
}