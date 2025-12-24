namespace AssetManagement.Entities.Models;

[Table("Addresses")]
public class AddressesModel : BaseModel
{
    [Required]
    [MaxLength(100)]
    public string Street { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string City { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string State { get; set; } = default!;

    [Required]
    [MaxLength(20)]
    public string PostalCode { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string Region { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string Country { get; set; } = default!; 

    // Navigation property (if tied to Asset, Vendor, or User)
    public Guid? QueryId { get; set; }
}