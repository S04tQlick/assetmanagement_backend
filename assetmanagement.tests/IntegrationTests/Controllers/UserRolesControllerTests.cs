using AssetManagement.API.Constants;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.FakeData;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers.ApiOperations;
using AssetManagement.Tests.Helpers.Operations;
using Xunit.Abstractions;

namespace AssetManagement.Tests.IntegrationTests.Controllers;

public class UserRolesControllerTests(ApplicationFixture fixture, ITestOutputHelper testOutputHelper) : UserRolesOperations(fixture), IClassFixture<ApplicationFixture> 
{
    private readonly ApplicationFixture _fixture = fixture;
    private readonly GlobalOperations _ops = new GlobalOperations(fixture); 
    
    [Fact]
    public async Task It_Should_Be_Healthy()
    {
        var response = await _fixture.Client.GetAsync(ApiPath.SetUsersControllerRoute(ControllerConstants.HealthRoute));
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var content = await response.Content.ReadAsStringAsync();
        var responseMessage = TestOperations.Deserialize<HealthResponse>(content);
        
        responseMessage?.Message.Should().Be(ControllerConstants.HealthMessage);
    }

    [Fact]
    public async Task It_Should_Create_UserRole()
    {
        //get institution from rolesModel
        var instRow = (await _ops.GetInstitutionsAsync())?.FirstOrDefault();
        instRow.Should().NotBeNull();
        
        //get roles from rolesModel
        var roleRow = (await _ops.GetRolesAsync())?.FirstOrDefault();
        roleRow.Should().NotBeNull();
        
        //get users from usersModel
        var userRow = (await _ops.GetUsersByInstitutionIdAsync(instRow.Id))?.FirstOrDefault();
        userRow.Should().NotBeNull();

        var request = UserRoleRequestFaker.GetCreateRequestFaker(userRow.Id, roleRow.Id).Generate();
        
        if(await UserRoleExistsAsync(request.UserId, request.RoleId))
            testOutputHelper.WriteLine($"User '{request.UserId}' is already assigned with role '{request.RoleId}', skipping creation.");

        else
        {
            var response = await _fixture.Client.PostAsync(ApiPath.SetUserRolesControllerRoute(),
                TestOperations.SetRequestBody(request));

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var content = await response.Content.ReadAsStringAsync();
            
            var responseMessage = TestOperations.Deserialize<UserRolesResponse>(content);

            responseMessage.Should().NotBeNull();
            responseMessage.UserId.Should().Be(request.UserId);
            responseMessage.RoleId.Should().Be(request.RoleId); 
            responseMessage.IsActive.Should().BeTrue();
            responseMessage.CreatedAt.Should().NotBe(default); 
        }
    } 
}