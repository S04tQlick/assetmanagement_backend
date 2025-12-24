namespace AssetManagement.Entities.DTOs.Requests;

public class AddressesCreateRequest
{
    public string? Street { get; set; } 
    public string? City { get; set; } 
    public string? State { get; set; } 
    public string? PostalCode { get; set; } 
    public string? Region { get; set; } 
    public string? Country { get; set; } 
    public Guid QueryId { get; set; }
}

public class AddressesUpdateRequest
{
    public string? Street { get; set; } 
    public string? City { get; set; } 
    public string? State { get; set; } 
    public string? PostalCode { get; set; } 
    public string? Region { get; set; } 
    public string? Country { get; set; } 
    public Guid QueryId { get; set; }
}