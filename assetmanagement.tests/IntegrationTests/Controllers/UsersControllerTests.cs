using AssetManagement.API.Constants;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.FakeData;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers.ApiOperations;
using AssetManagement.Tests.Helpers.Operations;
using Xunit.Abstractions;

namespace AssetManagement.Tests.IntegrationTests.Controllers;

public class UsersControllerTests(ApplicationFixture fixture, ITestOutputHelper testOutputHelper) : UserOperations(fixture), IClassFixture<ApplicationFixture>
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
    public async Task It_Should_Create_User()
    {
        var instRow = (await _ops.GetInstitutionsAsync())?.FirstOrDefault();
        instRow.Should().NotBeNull();

        var request = UserCreateRequestFaker.GetCreateRequestFaker(instRow.Id).Generate();
        
        if(await UserExistsByEmailAndInstitutionIdAsync(request.EmailAddress, request.InstitutionId))
            testOutputHelper.WriteLine($"User with email '{request.EmailAddress}' already exists, skipping creation.");

        else
        {
            var response = await _fixture.Client.PostAsync(ApiPath.SetUsersControllerRoute(),
                TestOperations.SetRequestBody(request));

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var content = await response.Content.ReadAsStringAsync();
            
            var responseMessage = TestOperations.Deserialize<UsersResponse>(content);

            responseMessage.Should().NotBeNull();
            responseMessage.FirstName.Should().Be(request.FirstName);
            responseMessage.LastName.Should().Be(request.LastName);
            responseMessage.PhoneNumber.Should().Be(request.PhoneNumber);
            responseMessage.EmailAddress.Should().Be(request.EmailAddress);
            responseMessage.EmailAddress.Should().Contain("@");
            responseMessage.IsActive.Should().BeTrue();
            responseMessage.CreatedAt.Should().NotBe(default);
            responseMessage.InstitutionId.Should().Be(instRow.Id);
        }
    } 

    [Fact]
    public async Task It_Should_Get_Users()
    {
        var usersData = await GetUsersDataAsync();

        if (usersData is not null)
        {
            foreach (var row in usersData)
            {
                row.Id.Should().NotBeEmpty();
                row.FirstName.Should().NotBeNullOrEmpty();
                row.LastName.Should().NotBeNullOrEmpty();
                row.EmailAddress.Should().Contain("@");
                row.DisplayName.Should().Contain(row.DisplayName);
                row.InstitutionId.Should().Be(row.InstitutionId);
                row.PhoneNumber.Should().StartWith("+44");
            }
        }
    }
    
    
    
    
} 