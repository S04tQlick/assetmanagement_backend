using AssetManagement.Entities.Models;

namespace AssetManagement.Entities.DTOs.Requests;

public class UsersCreateRequest : BaseModel
{
    [MaxLength(100)]
    public required string FirstName { get; set; }
    
    [MaxLength(100)]
    public required string LastName { get; set; }
    
    [EmailAddress]
    public required string EmailAddress { get; set; }
    
    [Phone]
    [MaxLength(20)]
    public required string PhoneNumber { get; set; }

    public required string PasswordHash { get; set; }
    public string? NormalizedEmail { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public bool IsEmailVerified { get; set; } 
    public bool IsPhoneConfirmed { get; set; }
    public DateTime? LastLogin { get; set; }
    public Guid InstitutionId { get; set; }
}

public class UsersUpdateRequest 
{ 
    [MaxLength(100)]
    public required string FirstName { get; set; }
    
    [MaxLength(100)]
    public required string LastName { get; set; }
    
    // [EmailAddress]
    // public required string EmailAddress { get; set; }
    
    [Phone]
    public required string PhoneNumber { get; set; }

    // [MaxLength(200)]
    // public required string DisplayName { get; set; }

    //public required string PasswordHash { get; set; }
    public string? NormalizedEmail { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public bool IsEmailVerified { get; set; } 
    public bool IsPhoneConfirmed { get; set; }
    public DateTime? LastLogin { get; set; }
    public Guid InstitutionId { get; set; } 
}