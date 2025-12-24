using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers.ApiOperations;

namespace AssetManagement.Tests.Helpers.Operations;

public abstract class RolesOperations(ApplicationFixture fixture)
{
    protected async Task<IEnumerable<RolesResponse>?> GetRolesAsync()
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetRolesControllerRoute());
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<RolesResponse>>(content);
    }
    
    protected async Task<bool> RolesExistsByNameAsync(string name)
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetRolesControllerRoute());
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var assetTypes = TestOperations.Deserialize<IEnumerable<RolesResponse>>(content);

        return assetTypes?.Any(a => 
            string.Equals(a.RoleName?.Trim().ToLower(), name.Trim().ToLower(), StringComparison.OrdinalIgnoreCase)
        ) ?? false;
    }
}
