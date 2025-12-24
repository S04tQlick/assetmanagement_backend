using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Tests.Fixtures;

namespace AssetManagement.Tests.Helpers.ApiOperations;

public class GlobalOperations(ApplicationFixture fixture)
{
    public async Task<IEnumerable<InstitutionsResponse>?> GetInstitutionsAsync()
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetInstitutionsControllerRoute());
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<InstitutionsResponse>>(content);
    }

    public async Task<IEnumerable<BranchesResponse>?> GetBranchesByInsitutionIdAsync(Guid institutionId)
    {
        var response =
            await fixture.Client.GetAsync(ApiPath.SetBranchesControllerRoute($"institution/{institutionId}"));
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<BranchesResponse>>(content);
    }

    public async Task<IEnumerable<AssetTypesResponse>?> GetAssetTypesAsync()
    {
        var response = await fixture.Client.GetAsync(
            ApiPath.SetAssetTypesControllerRoute()
        );

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<AssetTypesResponse>>(content);
    }

    public async Task<IEnumerable<AssetCategoriesResponse>?> GetAssetCategoriesByInstitutionAndAssetTypeAsync(
        Guid institutionId, Guid assetTypeId)
    {
        var response = await fixture.Client.GetAsync(
            ApiPath.SetAssetCategoriesControllerRoute(
                $"institution/{institutionId}/type/{assetTypeId}"
            )
        );

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<AssetCategoriesResponse>>(content);
    }

    public async Task<IEnumerable<VendorsResponse>?> GetVendorsByInstitutionIdAsync(Guid institutionId)
    {
        var response = await fixture.Client.GetAsync(
            ApiPath.SetVendorsControllerRoute(
                $"institution/{institutionId}"
            )
        );

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<VendorsResponse>>(content);
    }

    public async Task<IEnumerable<RolesResponse>?> GetRolesAsync()
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetRolesControllerRoute());
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<RolesResponse>>(content);
    }

    public async Task<IEnumerable<UsersResponse>?> GetUsersByInstitutionIdAsync(Guid institutionId)
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetUsersControllerRoute($"institution/{institutionId}"));
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<UsersResponse>>(content);
    }
}