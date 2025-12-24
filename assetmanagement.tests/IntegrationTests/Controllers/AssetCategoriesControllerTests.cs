using AssetManagement.API.Constants;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.FakeData;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers.ApiOperations;
using AssetManagement.Tests.Helpers.Operations;
using Xunit.Abstractions;


namespace AssetManagement.Tests.IntegrationTests.Controllers;

public class AssetCategoriesControllerTests(ApplicationFixture fixture, ITestOutputHelper testOutputHelper)
    : AssetCategoriesOperations(fixture), IClassFixture<ApplicationFixture>
{
    private readonly ApplicationFixture _fixture = fixture;
    private readonly GlobalOperations _ops = new GlobalOperations(fixture);
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    [Fact]
    public async Task It_Should_Be_Healthy()
    {
        var response = await _fixture.Client.GetAsync(ApiPath.SetAssetCategoriesControllerRoute(ControllerConstants.HealthRoute));
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        var responseMessage = TestOperations.Deserialize<HealthResponse>(content);

        responseMessage?.Message.Should().Be(ControllerConstants.HealthMessage);
    }

    [Fact]
    public async Task It_Should_Create_AssetCategories()
    {
        // Arrange: seed AssetTypes first
        var astTypeRow = (await _ops.GetAssetTypesAsync())?.FirstOrDefault(); 
        astTypeRow.Should().NotBeNull();

        var instRow = (await _ops.GetInstitutionsAsync())?.FirstOrDefault(); 
        instRow.Should().NotBeNull();

        // Generate a fake request
        var request = AssetCategoriesRequestFaker.GetCreateRequestFaker([astTypeRow], instRow.Id).Generate();
        
        if (await AssetCategoriesExistsByNameAndInstitutionIdAsync(request.AssetCategoryName, request.InstitutionId, request.AssetTypeId))
            _testOutputHelper.WriteLine(
                $"AssetCategory '{request.AssetCategoryName}' already exists, skipping creation.");

        else
        {
            // Act: post to API
            var response = await _fixture.Client.PostAsync(
                ApiPath.SetAssetCategoriesControllerRoute(),
                TestOperations.SetRequestBody(request)
            );

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var content = await response.Content.ReadAsStringAsync();
            var responseMessage = TestOperations.Deserialize<AssetCategoriesResponse>(content);

            responseMessage.Should().NotBeNull();
            responseMessage.Id.Should().NotBe(Guid.Empty);
            responseMessage.AssetCategoryName.Should().Be(request.AssetCategoryName);
            responseMessage.AssetTypeId.Should().Be(request.AssetTypeId);
            responseMessage.InstitutionId.Should().Be(request.InstitutionId);
            responseMessage.IsActive.Should().BeTrue();
            responseMessage.CreatedAt.Should().NotBe(default);
        }
    }

    [Fact]
    public async Task It_Should_Get_AssetCategories()
    {
        var institutionData = await _ops.GetInstitutionsAsync();
        var instRow = institutionData?.FirstOrDefault();
        instRow.Should().NotBeNull();
        
        var assetTypeData = await _ops.GetAssetTypesAsync();
        var assetTypeRow = assetTypeData?.FirstOrDefault();
        assetTypeRow.Should().NotBeNull();

        var assetCategoriesData = await _ops.GetAssetCategoriesByInstitutionAndAssetTypeAsync(instRow.Id, assetTypeRow.Id);
        if (assetCategoriesData is not null)
        {
            foreach (var row in assetCategoriesData)
            {
                row.Id.Should().NotBeEmpty();
                row.AssetCategoryName.Should().NotBeEmpty();
                row.InstitutionId.Should().Be(instRow.Id);

                if (row.IsActive.Equals(true))
                    row.IsActive.Should().BeTrue();
                else
                    row.IsActive.Should().BeFalse();

            }
        }
    }



}