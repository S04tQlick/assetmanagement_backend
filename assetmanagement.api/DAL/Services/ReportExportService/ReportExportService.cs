using System.Globalization;
using System.Text;
using AssetManagement.Entities.DTOs.Responses;
using CsvHelper;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace AssetManagement.API.DAL.Services.ReportExportService;

public class ReportExportService : IReportExportService
{
    public async Task<byte[]> GenerateCsvAsync(IEnumerable<DepreciationReportsResponse> data)
    {
        await using var memoryStream = new MemoryStream();
        await using var writer = new StreamWriter(memoryStream, Encoding.UTF8);
        await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        await csv.WriteRecordsAsync(data);
        await writer.FlushAsync();

        return memoryStream.ToArray();
    }

    public async Task<byte[]> GeneratePdfAsync(IEnumerable<DepreciationReportsResponse> data)
    {
        using var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont("Arial", 10, XFontStyle.Regular);

        double yPoint = 40;
        gfx.DrawString("Depreciation Report", new XFont("Arial", 14, XFontStyle.Bold), XBrushes.Black, new XRect(0, 20, page.Width, 20), XStringFormats.TopCenter);

        foreach (var record in data)
        {
            string line = $"{record.AssetName} | {record.InstitutionName} | {record.MethodEnum} | Value: {record.CurrentValue:C}";
            gfx.DrawString(line, font, XBrushes.Black, new XRect(40, yPoint, page.Width - 80, page.Height - 40), XStringFormats.TopLeft);
            yPoint += 18;

            if (yPoint > page.Height - 50)
            {
                page = document.AddPage();
                gfx = XGraphics.FromPdfPage(page);
                yPoint = 40;
            }
        }

        await using var stream = new MemoryStream();
        document.Save(stream, false);
        return stream.ToArray();
    }
}