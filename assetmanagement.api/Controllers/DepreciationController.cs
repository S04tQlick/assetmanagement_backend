// using AssetManagement.API.DAL.Repositories.AssetsRepository;
// using AssetManagement.API.DAL.Services.DepreciationService;
// using AssetManagement.API.DAL.Services.ReportExportService;
// using AssetManagement.Entities.DTOs.Requests;
// using AssetManagement.Entities.DTOs.Responses;
// using Microsoft.AspNetCore.Mvc;
//
// namespace AssetManagement.API.Controllers;
//
// [ApiController]
// [Route("api/[controller]")]
// public class DepreciationController(IAssetRepository assetRepo, IDepreciationService depreciationService, IReportExportService reportExportService) : ControllerBase
// {
//     [HttpPost("report")]
//     public async Task<ActionResult<IEnumerable<DepreciationReportsResponse>>> GetReport([FromBody] DepreciationsReportRequest request)
//     {
//         var assets = await assetRepo.GetFilteredAsync(
//             request.InstitutionId,
//             request.BranchId,
//             request.CategoryId,
//             request.Method,
//             request.FromDate,
//             request.ToDate);
//
//         var reports = new List<DepreciationReportsResponse>();
//
//         foreach (var asset in assets)
//         {
//             var annual = depreciationService.CalculateAnnualDepreciation(asset);
//             var current = depreciationService.CalculateCurrentValue(asset);
//             var accumulated = asset.PurchasePrice - current;
//
//             reports.Add(new DepreciationReportsResponse
//             {
//                 AssetId = asset.Id,
//                 AssetName = asset.Name,
//                 InstitutionName = asset.Institution?.Name ?? "",
//                 BranchName = asset.Branch?.Name,
//                 CategoryName = asset.AssetCategory?.AssetCategoryName,
//                 MethodEnum = asset.DepreciationMethodEnum,
//                 PurchasePrice = asset.PurchasePrice,
//                 SalvageValue = asset.SalvageValue,
//                 UsefulLifeYears = asset.UsefulLifeYears,
//                 AnnualDepreciation = annual,
//                 AccumulatedDepreciation = accumulated,
//                 CurrentValue = current,
//                 PurchaseDate = asset.PurchaseDate,
//                 NextMaintenanceDate = asset.NextMaintenanceDate
//             });
//         }
//
//         return Ok(reports.OrderBy(r => r.InstitutionName).ThenBy(r => r.AssetName));
//     }
//     
//     [HttpPost("report/export")]
//     public async Task<IActionResult> ExportReport([FromBody] DepreciationsReportRequest request, [FromQuery] string format = "csv")
//     {
//         var assets = await assetRepo.GetFilteredAsync(
//             request.InstitutionId,
//             request.BranchId,
//             request.CategoryId,
//             request.Method,
//             request.FromDate,
//             request.ToDate);
//
//         var reports = assets.Select(asset =>
//         {
//             var annual = depreciationService.CalculateAnnualDepreciation(asset);
//             var current = depreciationService.CalculateCurrentValue(asset);
//             var accumulated = asset.PurchasePrice - current;
//
//             return new DepreciationReportsResponse
//             {
//                 AssetId = asset.Id,
//                 AssetName = asset.Name,
//                 InstitutionName = asset.Institution?.Name ?? "",
//                 BranchName = asset.Branch?.Name,
//                 CategoryName = asset.AssetCategory?.AssetCategoryName,
//                 MethodEnum = asset.DepreciationMethodEnum,
//                 PurchasePrice = asset.PurchasePrice,
//                 SalvageValue = asset.SalvageValue,
//                 UsefulLifeYears = asset.UsefulLifeYears,
//                 AnnualDepreciation = annual,
//                 AccumulatedDepreciation = accumulated,
//                 CurrentValue = current,
//                 PurchaseDate = asset.PurchaseDate,
//                 NextMaintenanceDate = asset.NextMaintenanceDate
//             };
//         }).ToList();
//
//         var bytes = format.ToLower() switch
//         {
//             "pdf" => await reportExportService.GeneratePdfAsync(reports),
//             _ => await reportExportService.GenerateCsvAsync(reports)
//         };
//
//         var mime = format.ToLower() == "pdf" ? "application/pdf" : "text/csv";
//         var fileName = $"DepreciationReport_{DateTime.UtcNow:yyyyMMddHHmmss}.{format}";
//
//         return File(bytes, mime, fileName);
//     }
// }