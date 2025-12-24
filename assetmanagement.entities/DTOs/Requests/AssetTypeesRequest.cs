using AssetManagement.Entities.Models;

namespace AssetManagement.Entities.DTOs.Requests;

public class AssetTypesCreateRequest : BaseModel
{
    public required string AssetTypeName { get; set; }
    public required string Description { get; set; }
}

public class AssetTypesUpdateRequest 
{ 
    public required string AssetTypeName { get; set; }
    public required string Description { get; set; } 
}