using AssetManagement.Entities.Models;

namespace AssetManagement.Entities.DTOs.Responses;

public class AssetTypesResponse : BaseResponse
{ 
    public required string AssetTypeName { get; init; }
    public required string Description { get; init; } 
}