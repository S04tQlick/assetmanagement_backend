using AssetManagement.API.Constants;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Enums;
using AssetManagement.Entities.FakeData;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers;
using AssetManagement.Tests.Helpers.ApiOperations;
using AssetManagement.Tests.Helpers.Operations;
using SixLabors.ImageSharp;


namespace AssetManagement.Tests.IntegrationTests.Controllers;

public class BranchesControllerTests(ApplicationFixture fixture)
    : BranchOperations(fixture), IClassFixture<ApplicationFixture>
{
    private readonly ApplicationFixture _fixture = fixture;
    private readonly GlobalOperations _ops = new GlobalOperations(fixture);

    [Fact]
    public async Task It_Should_Be_Healthy()
    {
        var response =
            await _fixture.Client.GetAsync(ApiPath.SetBranchesControllerRoute(ControllerConstants.HealthRoute));
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        var responseMessage = TestOperations.Deserialize<HealthResponse>(content);

        responseMessage?.Message.Should().Be(ControllerConstants.HealthMessage);
    }

    [Fact]
    public async Task It_Should_Create_Branch()
    {
        var institutionData = await _ops.GetInstitutionsAsync();
        var row = institutionData?.FirstOrDefault();
        row.Should().NotBeNull();

        var request = BranchRequestFaker.GetCreateRequestFaker(row.Id).Generate();

        var response = await _fixture.Client.PostAsync(
            ApiPath.SetBranchesControllerRoute(),
            TestOperations.SetRequestBody(request)
        );

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var content = await response.Content.ReadAsStringAsync();
        var responseMessage = TestOperations.Deserialize<BranchesResponse>(content);

        responseMessage.Should().NotBeNull();
        responseMessage.BranchName.Should().Be(request.BranchName);
        responseMessage.IsActive.Should().BeTrue();
        responseMessage.CreatedAt.Should().NotBe(default);
        responseMessage.InstitutionId.Should().Be(row.Id);
    }

    [Fact]
    public async Task It_Should_Get_Branches()
    {
        var branchesData = await GetBranchesDataAsync();
    
        if (branchesData is not null)
        {
            foreach (var row in branchesData)
            {
                row.Id.Should().NotBeEmpty();
                row.BranchName.Should().NotBeEmpty(); 
                row.BranchName.Should().NotBeNullOrWhiteSpace();
                
                row.Latitude.Should().NotBe(0);
                row.Longitude.Should().NotBe(0);
                
                row.IsActive.Should().Be(row.IsActive);
                row.IsHeadOffice.Should().Be(row.IsHeadOffice);
                
                row.InstitutionId.Should().NotBe(row.Id);
            }
        }
    }



}