using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers.ApiOperations;

namespace AssetManagement.Tests.Helpers.Operations;

public abstract class BranchOperations(ApplicationFixture fixture)
{
    protected async Task<IEnumerable<BranchesResponse>?> GetBranchesDataAsync()
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetBranchesControllerRoute());
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<BranchesResponse>>(content);
    }

    protected async Task<IEnumerable<BranchesResponse>?> GetBranchesByInstitutionIdAsync(string insitutionId)
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetBranchesControllerRoute(insitutionId));
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<BranchesResponse>>(content);
    }
}