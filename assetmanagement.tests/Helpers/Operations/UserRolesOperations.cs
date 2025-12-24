using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers.ApiOperations;

namespace AssetManagement.Tests.Helpers.Operations;

public class UserRolesOperations (ApplicationFixture fixture)
{
    protected async Task<IEnumerable<UserRolesResponse>?> GetUserRolesDataAsync()
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetUserRolesControllerRoute());
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return TestOperations.Deserialize<IEnumerable<UserRolesResponse>>(content);
    }

    protected async Task<bool> UserRoleExistsAsync(Guid userId, Guid roleId)
    {
        var response = await fixture.Client.GetAsync(ApiPath.SetUserRolesControllerRoute($"user/{userId}"));
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var userRoles = TestOperations.Deserialize<IEnumerable<UserRolesResponse>>(content);

        return userRoles?.Any(u => u.UserId == userId && u.RoleId == roleId) ?? false;
    }
}