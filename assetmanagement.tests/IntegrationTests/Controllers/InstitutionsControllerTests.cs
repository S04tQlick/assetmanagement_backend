using AssetManagement.API.Constants;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.FakeData;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers.ApiOperations;
using AssetManagement.Tests.Helpers.Operations;


namespace AssetManagement.Tests.IntegrationTests.Controllers;

public class InstitutionsControllerTests(ApplicationFixture fixture)
    : InstitutionOperations(fixture), IClassFixture<ApplicationFixture>
{ 
    private readonly ApplicationFixture _fixture = fixture;

    [Fact]
    public async Task It_Should_Be_Healthy()
    {
        var response = await _fixture.Client.GetAsync(ApiPath.SetInstitutionsControllerRoute(ControllerConstants.HealthRoute));
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        var responseMessage = TestOperations.Deserialize<HealthResponse>(content);

        responseMessage?.Message.Should().Be(ControllerConstants.HealthMessage);
    }
    
    [Fact]
    public void PrimaryColor_Should_Be_Valid_Hex_Code()
    {
        var fakeRequest = InstitutionRequestFaker.GetCreateRequestFaker().Generate(); 

        var primaryColor = fakeRequest.PrimaryColor;

        Assert.NotNull(primaryColor);
        Assert.Matches("^#([A-Fa-f0-9]{6})$", primaryColor); 
    }

    [Fact]
    public void SecondaryColor_Should_Be_Valid_Hex_Code()
    {
        var faker = InstitutionRequestFaker.GetCreateRequestFaker();
        var fakeRequest = faker.Generate();

        var secondaryColor = fakeRequest.SecondaryColor;

        Assert.NotNull(secondaryColor);
        Assert.Matches("^#([A-Fa-f0-9]{6})$", secondaryColor);
    }
    
    [Fact]
    public async Task It_Should_Create_Institution()
    {
        var request = InstitutionRequestFaker.GetCreateRequestFaker().Generate();
        var response = await _fixture.Client.PostAsync(
            ApiPath.SetInstitutionsControllerRoute(),
            TestOperations.SetRequestBody(request)
        );
        
        response.StatusCode.Should().Be(HttpStatusCode.Created); 
        
        var content = await response.Content.ReadAsStringAsync();
        var responseMessage = TestOperations.Deserialize<InstitutionsResponse>(content);
        
        responseMessage.Should().NotBeNull();
        responseMessage.InstitutionName.Should().Be(request.InstitutionName);
        responseMessage.IsActive.Should().BeTrue();
        responseMessage.CreatedAt.Should().NotBe(default); 
    }
    
    [Fact]
    public async Task It_Should_Get_Institutions_By_Date()
    {
        var institutionsData = await GetInstitutionByDateAsync(DateTime.UtcNow.ToString("o"));

        if (institutionsData is not null)
        {
            foreach (var row in institutionsData)
            {
                row.Id.Should().NotBeEmpty();
                row.InstitutionName.Should().NotBeEmpty();
                row.InstitutionEmail.Should().NotBeEmpty();
                row.InstitutionContactNumber.Should().NotBeEmpty();
                row.PrimaryColor.Should().NotBeEmpty();
                row.SecondaryColor.Should().NotBeEmpty();
                row.LogoSanityId.Should().NotBeEmpty();
                row.LogoUrl.Should().NotBeEmpty();
                row.IsActive.Should().BeTrue(); 
            }
        }
    }

    [Fact]
    public async Task It_Should_Get_Institution_By_Id()
    {
        var institutionData = await GetInstitutionAsync();

        var row = institutionData?.FirstOrDefault();
        row.Should().NotBeNull();

        var response = await _fixture.Client.GetAsync(ApiPath.SetInstitutionsControllerRoute($"{row.Id}"));
        
        var content = await response.Content.ReadAsStringAsync();
        var responseMessage = TestOperations.Deserialize<InstitutionsResponse>(content);

        responseMessage.Should().NotBeNull();
    }

    [Fact]
    public async Task It_Should_Update_Institution()
    {
        var institutionData = await GetInstitutionAsync();

        var row = institutionData?.FirstOrDefault();
        row.Should().NotBeNull();

        var request = InstitutionRequestFaker.GetCreateRequestFaker().Generate();
        request.Id = row.Id;
        request.CreatedAt = row.CreatedAt;
        request.UpdatedAt = DateTime.UtcNow;
        request.IsActive = row.IsActive;

        var response = await _fixture.Client.PutAsync(
            ApiPath.SetInstitutionsControllerRoute($"{row.Id}"),
            TestOperations.SetRequestBody(request)
        );
        
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        var getResponse = await _fixture.Client.GetAsync(ApiPath.SetInstitutionsControllerRoute(row.Id.ToString()));
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await getResponse.Content.ReadAsStringAsync();
        var updated = TestOperations.Deserialize<InstitutionsResponse>(content);

        updated.Should().NotBeNull();
        updated.InstitutionName.Should().Be(request.InstitutionName);
    }

    // [Fact]
    // public async Task It_Should_Delete_Institution()
    // {
    //     var request = new InstitutionCreateRequest
    //     {
    //         Name = "Delete Institution",
    //         Email = "delete@institution.com",
    //         ContactNumber = "+441234567895"
    //     };
    //
    //     var createResponse = await _fixture.Client.PostAsJsonAsync("/api/institutions", request);
    //     createResponse.EnsureSuccessStatusCode();
    //
    //     var created = await createResponse.Content.ReadFromJsonAsync<InstitutionResponse>();
    //     Assert.NotNull(created);
    //
    //     var deleteResponse = await _fixture.Client.DeleteAsync($"/api/institutions/{created!.Id}");
    //     Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    //
    //     var getResponse = await _fixture.Client.GetAsync($"/api/institutions/{created.Id}");
    //     Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    // }
}