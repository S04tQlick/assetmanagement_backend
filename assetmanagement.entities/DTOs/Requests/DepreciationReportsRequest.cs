using AssetManagement.Entities.Enums;

namespace AssetManagement.Entities.DTOs.Requests;

public class DepreciationsReportRequest
{
    public Guid? InstitutionId { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? CategoryId { get; set; }
    public DepreciationMethodEnum? Method { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}