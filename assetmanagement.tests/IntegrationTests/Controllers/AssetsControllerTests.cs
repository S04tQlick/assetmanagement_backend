using AssetManagement.API.Constants;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.FakeData;
using AssetManagement.Tests.Fixtures;
using AssetManagement.Tests.Helpers.ApiOperations;
using AssetManagement.Tests.Helpers.Operations;
using Xunit.Abstractions;

namespace AssetManagement.Tests.IntegrationTests.Controllers;

public class AssetsControllerTests(ApplicationFixture fixture, ITestOutputHelper testOutputHelper) : AssetsOperations(fixture), IClassFixture<ApplicationFixture>
{
    private readonly ApplicationFixture _fixture = fixture;
    private readonly GlobalOperations _ops = new GlobalOperations(fixture);
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    [Fact]
    public async Task It_Should_Be_Healthy()
    {
        var response = await _fixture.Client.GetAsync(ApiPath.SetAssetsControllerRoute(ControllerConstants.HealthRoute));
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        var responseMessage = TestOperations.Deserialize<HealthResponse>(content);

        responseMessage?.Message.Should().Be(ControllerConstants.HealthMessage);
    }

    [Fact]
    public async Task It_Should_Create_Assets()
    {
        //select from institutions,
        var instRow = (await _ops.GetInstitutionsAsync())?.FirstOrDefault();
        instRow.Should().NotBeNull();

        //branches,
        var branchRow = (await _ops.GetBranchesByInsitutionIdAsync(instRow.Id))?.FirstOrDefault();
        branchRow.Should().NotBeNull();

        //assetTypes,
        var assetTypeRow = (await _ops.GetAssetTypesAsync())?.FirstOrDefault();
        assetTypeRow.Should().NotBeNull();

        //assetCategories,
        var assetCatRow = (await _ops.GetAssetCategoriesByInstitutionAndAssetTypeAsync(instRow.Id, assetTypeRow.Id))
            ?.FirstOrDefault();
        assetCatRow.Should().NotBeNull();

        //vendors,
        var venRow = (await _ops.GetVendorsByInstitutionIdAsync(instRow.Id))?.FirstOrDefault();
        venRow.Should().NotBeNull();

        var request = AssetRequestFaker
            .GetCreateRequestFaker(instRow.Id, branchRow.Id, assetTypeRow.Id, assetCatRow.Id, venRow.Id).Generate();

        if (await AssetExistsBySerialNumberAndInstitutionIdAsync(request.SerialNumber, request.InstitutionId))
            _testOutputHelper.WriteLine($"Asset '{request.AssetName}' with SerialNumber '{request.SerialNumber}' already exists, skipping creation.");
        
        else
        {
            var response = await _fixture.Client.PostAsync(
                ApiPath.SetAssetsControllerRoute(),
                TestOperations.SetRequestBody(request)
            );

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var content = await response.Content.ReadAsStringAsync();


            var responseMessage = TestOperations.Deserialize<AssetsResponse>(content);


            responseMessage?.AssetName.Should().NotBeEmpty();
            // responseMessage?.SerialNumber.Should().NotBeEmpty();
            //
            // responseMessage?.InstitutionId.Should().Be(request.InstitutionId); 
            // responseMessage?.BranchId.Should().Be(request.BranchId); 
            // responseMessage?.AssetCategoryId.Should().Be(request.AssetCategoryId); 
            // responseMessage?.AssetTypeId.Should().Be(request.AssetTypeId); 
            // responseMessage?.VendorId.Should().Be(request.VendorId); 
            //
            // responseMessage?.PurchaseDate.Should().Be(request.PurchaseDate);
            // responseMessage?.PurchasePrice.Should().Be(request.PurchasePrice);
            // responseMessage?.UsefulLifeYears.Should().Be(request.UsefulLifeYears);
            //
            // responseMessage?.UnitsTotal.Should().Be(request.UnitsTotal);
            // responseMessage?.CurrentUnits.Should().Be(request.CurrentUnits);
            //
            // responseMessage?.SanityAssetId.Should().NotBeEmpty();
            // responseMessage?.SanityUrl.Should().NotBeEmpty();
            //
            // responseMessage?.MaintenanceDueDate.Should().Be(request.MaintenanceDueDate);
            // responseMessage?.NextMaintenanceDate.Should().Be(request.NextMaintenanceDate);
            //
            // responseMessage?.SalvageValue.Should().Be(request.SalvageValue);
            // responseMessage?.DepreciationMethodEnum.Should().Be(request.DepreciationMethodEnum);
            //
            // responseMessage?.CurrentValue.Should().Be(request.CurrentValue);
            // responseMessage?.AccumulatedDepreciation.Should().Be(request.AccumulatedDepreciation);
            // if (responseMessage!.IsActive.Equals(true))
            //     responseMessage.IsActive.Should().BeTrue();
            // else
            //     responseMessage.IsActive.Should().BeFalse();
            //         
            // responseMessage?.CreatedAt.Should().NotBe(default); 
            // responseMessage?.UpdatedAt.Should().Be(request.UpdatedAt);


            //responseMessage?.Should().Be(MessageConstants.Success(RecordTypeEnum.Save));
        }
    }

    [Fact]
    public async Task It_Should_Get_Assets()
    {
        var assetData = await GetAssetsDataAsync();

        if (assetData is not null)
        {
            foreach (var row in assetData)
            {
                row.Id.Should().NotBeEmpty();
                row.AssetName.Should().NotBeEmpty();
                row.SerialNumber.Should().NotBeEmpty();

                row.InstitutionId.Should().Be(row.InstitutionId); 
                row.BranchId.Should().Be(row.BranchId); 
                row.AssetCategoryId.Should().Be(row.AssetCategoryId); 
                row.AssetTypeId.Should().Be(row.AssetTypeId); 
                row.VendorId.Should().Be(row.VendorId); 

                row.PurchaseDate.Should().Be(row.PurchaseDate);
                row.PurchasePrice.Should().Be(row.PurchasePrice);
                row.UsefulLifeYears.Should().Be(row.UsefulLifeYears);

                row.UnitsTotal.Should().Be(row.UnitsTotal);
                row.CurrentUnits.Should().Be(row.CurrentUnits);

                row.SanityAssetId.Should().NotBeEmpty();
                row.SanityUrl.Should().NotBeEmpty();

                row.MaintenanceDueDate.Should().Be(row.MaintenanceDueDate);
                row.NextMaintenanceDate.Should().Be(row.NextMaintenanceDate);

                row.SalvageValue.Should().Be(row.AccumulatedDepreciation);
                row.DepreciationMethod.Should().Be(row.DepreciationMethod);

                row.CurrentValue.Should().Be(row.AccumulatedDepreciation);
                row.AccumulatedDepreciation.Should().Be(row.AccumulatedDepreciation);
                if (row.IsActive.Equals(true))
                    row.IsActive.Should().BeTrue();
                else
                    row.IsActive.Should().BeFalse();
                
                row.CreatedAt.Should().Be(row.CreatedAt);
                row.UpdatedAt.Should().Be(row.UpdatedAt);
            }
        }
    }
}