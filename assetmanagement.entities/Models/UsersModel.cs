using AssetManagement.Entities.GeneralResponse;

namespace AssetManagement.Entities.Models;

[Table("Users")]
public class UsersModel : BaseModel, IInstitutionOwned
{
    private readonly string _email = string.Empty;

    [MaxLength(100)]
    public required string FirstName { get; set; }
    
    [MaxLength(100)]
    public required string LastName { get; set; }

    [EmailAddress]
    public required string EmailAddress
    {
        get => _email;
        init
        {
            _email = value;
            NormalizedEmail = value.Trim().ToUpperInvariant();
        }
    }
    
    [Phone]
    public required string PhoneNumber { get; set; }

    [MaxLength(200)]
    public required string DisplayName { get; set; }

    public required string PasswordHash { get; set; }
    public string? NormalizedEmail { get; private set; }
    public bool IsEmailConfirmed { get; set; }
    public bool IsEmailVerified { get; set; } 
    public bool IsPhoneConfirmed { get; set; }
    public DateTime? LastLogin { get; set; }
    public Guid InstitutionId { get; set; }
    
    [ForeignKey(nameof (InstitutionId))]
    public InstitutionsModel? Institutions { get; set; }
}