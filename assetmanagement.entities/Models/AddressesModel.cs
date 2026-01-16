namespace AssetManagement.Entities.Models;

[Table("Addresses")]
public class AddressesModel : BaseModel
{
    [Required]
    [MaxLength(100)]
    public required string Street { get; set; }

    [Required]
    [MaxLength(50)]
    public required string City { get; set; }

    [Required]
    [MaxLength(50)]
    public required string State { get; set; }

    [Required]
    [MaxLength(20)]
    public string? PostalCode { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Region { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Country { get; set; }

    // Navigation property (if tied to Asset, Vendor, or User)
    public Guid? QueryId { get; set; }
}