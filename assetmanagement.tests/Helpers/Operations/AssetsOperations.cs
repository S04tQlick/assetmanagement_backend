using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers.ApiOperations;

namespace AssetManagement.Tests.Helpers.Operations;

public class AssetsOperations (ApplicationFixture fixture)
{
    protected async Task<IEnumerable<AssetsResponse>?> GetAssetsDataAsync()
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetAssetsControllerRoute());
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<AssetsResponse>>(content);
    }

    protected async Task<IEnumerable<AssetsResponse>?> GetAssetsByInstitutionIdAsync(string insitutionId)
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetAssetsControllerRoute(insitutionId));
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<AssetsResponse>>(content);
    }
    
    protected async Task<bool> AssetExistsBySerialNumberAndInstitutionIdAsync(string serialNumber, Guid institutionId)
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetAssetsControllerRoute($"institution/{institutionId}"));
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var assets = TestOperations.Deserialize<IEnumerable<AssetsResponse>>(content);

        return assets?.Any(a => 
            string.Equals(a.SerialNumber.Trim().ToLower(), serialNumber.Trim().ToLower(), StringComparison.OrdinalIgnoreCase)
        ) ?? false;
    }
}