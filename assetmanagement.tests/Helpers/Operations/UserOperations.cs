using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers.ApiOperations;


namespace AssetManagement.Tests.Helpers.Operations;

public class UserOperations(ApplicationFixture fixture)
{
    protected async Task<IEnumerable<UsersResponse>?> GetUsersDataAsync()
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetUsersControllerRoute());
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<UsersResponse>>(content);
    }

    public async Task<IEnumerable<UsersResponse>?> GetUsersByInstitutionIdAsync(string insitutionId)
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetUsersControllerRoute($"{insitutionId}"));
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<UsersResponse>>(content);
    }

    protected async Task<bool> UserExistsByEmailAndInstitutionIdAsync(string email, Guid institutionId)
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetUsersControllerRoute($"institution/{institutionId}"));
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var users = TestOperations.Deserialize<IEnumerable<UsersResponse>>(content);

        return users?.Any(a =>
            string.Equals(a.EmailAddress, email, StringComparison.OrdinalIgnoreCase)
        ) ?? false;
    }
}