using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers.ApiOperations;

namespace AssetManagement.Tests.Helpers.Operations;

public class InstitutionOperations(ApplicationFixture fixture)
{
    protected async Task<IEnumerable<InstitutionsResponse>?> GetInstitutionAsync()
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetInstitutionsControllerRoute());
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<InstitutionsResponse>>(content);
    }

    protected async Task<IEnumerable<InstitutionsResponse>?> GetInstitutionByDateAsync(string date)
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetInstitutionsControllerRoute(date));
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<InstitutionsResponse>>(content);
    }
}