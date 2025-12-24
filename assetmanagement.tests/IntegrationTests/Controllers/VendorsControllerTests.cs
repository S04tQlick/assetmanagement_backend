using AssetManagement.API.Constants;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.FakeData;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers.ApiOperations;
using AssetManagement.Tests.Helpers.Operations;
using Xunit.Abstractions;

namespace AssetManagement.Tests.IntegrationTests.Controllers;

public class VendorsControllerTests(ApplicationFixture fixture, ITestOutputHelper testOutputHelper)
    : VendorOperations(fixture), IClassFixture<ApplicationFixture>
{
    private readonly ApplicationFixture _fixture = fixture;
    private readonly GlobalOperations _ops = new GlobalOperations(fixture); 

    [Fact]
    public async Task It_Should_Be_Healthy()
    {
        var response = await _fixture.Client.GetAsync(ApiPath.SetVendorsControllerRoute(ControllerConstants.HealthRoute));
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        var responseMessage = TestOperations.Deserialize<HealthResponse>(content);

        responseMessage?.Message.Should().Be(ControllerConstants.HealthMessage);
    }

    [Fact]
    public async Task It_Should_Create_Vendor()
    {
        var instRow = (await _ops.GetInstitutionsAsync())?.FirstOrDefault(); 
        instRow.Should().NotBeNull();

        var request = VendorCreateRequestFaker.GetCreateRequestFaker(instRow.Id).Generate();

        if (await VendorExistsByNameAndInstitutionIdAsync(request.VendorsName, request.InstitutionId))
            testOutputHelper.WriteLine($"Vendor '{request.VendorsName}' already exists, skipping creation.");

        else
        {
            var response = await _fixture.Client.PostAsync(
                ApiPath.SetVendorsControllerRoute(),
                TestOperations.SetRequestBody(request)
            );

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var content = await response.Content.ReadAsStringAsync();
            var responseMessage = TestOperations.Deserialize<VendorsResponse>(content);

            responseMessage.Should().NotBeNull();
            responseMessage.VendorsName.Should().Be(request.VendorsName);
            responseMessage.ContactInfo.Should().Be(request.ContactInfo);
            responseMessage.EmailAddress.Should().Be(request.EmailAddress);
            responseMessage.IsActive.Should().BeTrue();
            responseMessage.CreatedAt.Should().NotBe(default);
            responseMessage.InstitutionId.Should().Be(instRow.Id);

            //responseMessage.Should().Be(MessageConstants.Success(RecordTypeEnum.Save));
        }
    }

    [Fact]
    public async Task It_Should_Get_Vendors()
    {
        var vendorsData = await GetVendorsDataAsync();

        if (vendorsData is not null)
        {
            foreach (var row in vendorsData)
            {
                row.Id.Should().NotBeEmpty();
                row.VendorsName.Should().NotBeEmpty();
                row.EmailAddress.Should().NotBeEmpty();
                row.ContactInfo.Should().NotBeEmpty();
                row.IsActive.Should().BeTrue();
                row.InstitutionId.Should().NotBe(row.Id);
            }
        }
    }
}
    
    
    