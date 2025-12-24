using AssetManagement.Entities.DTOs.Responses;

namespace AssetManagement.API.DAL.Services.ReportExportService;

public interface IReportExportService
{
    Task<byte[]> GenerateCsvAsync(IEnumerable<DepreciationReportsResponse> data);
    Task<byte[]> GeneratePdfAsync(IEnumerable<DepreciationReportsResponse> data);
}