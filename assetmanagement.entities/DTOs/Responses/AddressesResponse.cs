using AssetManagement.Entities.Models;

namespace AssetManagement.Entities.DTOs.Responses;

public class AddressesResponse : BaseResponse
{
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public string Region { get; set; } = default!;
    public string Country { get; set; } = default!;
    public Guid QueryId { get; set; }
}