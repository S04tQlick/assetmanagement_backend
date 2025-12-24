using AssetManagement.API.Constants; 
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.FakeData;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers.ApiOperations;
using AssetManagement.Tests.Helpers.Operations;
using Microsoft.AspNetCore.Mvc;
using Xunit.Abstractions;

namespace AssetManagement.Tests.IntegrationTests.Controllers;

public class AssetTypesControllerTests(ApplicationFixture fixture, ITestOutputHelper testOutputHelper)
    : AssetTypeOperations(fixture), IClassFixture<ApplicationFixture>
{
    private readonly ApplicationFixture _fixture = fixture; 
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    [Fact]
    public async Task It_Should_Be_Healthy()
    {
        var response = await _fixture.Client.GetAsync(ApiPath.SetAssetTypesControllerRoute(ControllerConstants.HealthRoute));
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        var responseMessage = TestOperations.Deserialize<HealthResponse>(content);

        responseMessage?.Message.Should().Be(ControllerConstants.HealthMessage);
    }

    [Fact]
    public async Task It_Should_Add_AssetType()
    {
        var request = AssetTypeRequestFaker.GetCreateRequestFaker().Generate();
        if (await AssetTypesExistsByNameAsync(request.AssetTypeName))
            _testOutputHelper.WriteLine($"AssetType '{request.AssetTypeName}' already exists, skipping creation.");
        
        else
        {
            var response = await _fixture.Client.PostAsync(
                ApiPath.SetAssetTypesControllerRoute(),
                TestOperations.SetRequestBody(request)
            );

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var content = await response.Content.ReadAsStringAsync();
            var responseMessage = TestOperations.Deserialize<AssetTypesResponse>(content);

            responseMessage.Should().NotBeNull();
            responseMessage.AssetTypeName.Should().Be(request.AssetTypeName);
            responseMessage.IsActive.Should().BeTrue();
            responseMessage.CreatedAt.Should().NotBe(default);
        }
    }

    [Fact]
    public async Task It_Should_Return_NotFound_By_Incorrect_RowId()
    {
        var response = await _fixture.Client.GetAsync(ApiPath.SetAssetTypesControllerRoute($"{Guid.NewGuid()}"));
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var content = await response.Content.ReadAsStringAsync();
        var responseMessage = TestOperations.Deserialize<ProblemDetails>(content);

        responseMessage.Should().NotBeNull();
        responseMessage.Status.Should().Be((int)HttpStatusCode.NotFound);
        responseMessage.Title.Should().NotBeNullOrWhiteSpace();
    }
}