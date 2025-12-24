using AssetManagement.API.Constants;
using AssetManagement.Entities.DTOs.Responses; 
using AssetManagement.Entities.FakeData;
using AssetManagement.Tests.Fixtures; 
using AssetManagement.Tests.Helpers.ApiOperations;
using AssetManagement.Tests.Helpers.Operations;
using Xunit.Abstractions;

namespace AssetManagement.Tests.IntegrationTests.Controllers;

public class RolesControllerTest (ApplicationFixture fixture, ITestOutputHelper testOutputHelper) : RolesOperations(fixture), IClassFixture<ApplicationFixture>
{
    private readonly ApplicationFixture _fixture = fixture;
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    [Fact]
    public async Task It_Should_Be_Healthy()
    {
        var response = await _fixture.Client.GetAsync(ApiPath.SetRolesControllerRoute(ControllerConstants.HealthRoute));
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var content = await response.Content.ReadAsStringAsync();
        var responseMessage = TestOperations.Deserialize<HealthResponse>(content);
        
        responseMessage?.Message.Should().Be(ControllerConstants.HealthMessage);
    }
    
    [Fact]
    public async Task It_Should_Add_Roles()
    {
        var request = RolesRequestFaker.GetCreateRequestFaker().Generate();
        if (await RolesExistsByNameAsync(request.RoleName))
            _testOutputHelper.WriteLine($"RoleName '{request.RoleName}' already exists, skipping creation.");
        
        else
        {
            var response = await _fixture.Client.PostAsync(
                ApiPath.SetAssetTypesControllerRoute(),
                TestOperations.SetRequestBody(request)
            );

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var content = await response.Content.ReadAsStringAsync();
            var responseMessage = TestOperations.Deserialize<RolesResponse>(content);

            responseMessage.Should().NotBeNull();
            responseMessage.RoleName.Should().Be(request.RoleName);
            responseMessage.IsActive.Should().BeTrue();
            responseMessage.CreatedAt.Should().NotBe(default);
        }
    }

    [Fact]
    public async Task It_Should_Get_Roles()
    {
        var rolesData = await GetRolesAsync();
        if (rolesData is not null)
        {
            foreach (var row in rolesData)
            {
                row.Id.Should().NotBeEmpty();
                row.RoleName.Should().NotBeEmpty();
                row.IsActive.Should().BeTrue();
            }
        }
    } 
}