using AssetManagement.Entities.DTOs.Errors.ErrorResponses;

namespace AssetManagement.Entities.DTOs.Errors.ErrorRequests;

public class ValidationErrorResponse : ErrorResponse
{
    public IDictionary<string, string[]>? Errors { get; set; }
}