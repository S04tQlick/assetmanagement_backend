namespace AssetManagement.Entities.DTOs.Responses;

public class UsersResponse
{
    public Guid Id { get; init; }

    [MaxLength(100)]
    public required string FirstName { get; init; }
    
    [MaxLength(100)]
    public required string LastName { get; init; }
    
    [EmailAddress]
    public required string EmailAddress { get; init; }
    
    [Phone]
    public required string PhoneNumber { get; init; }

    [MaxLength(200)]
    public required string DisplayName { get; init; }

    public required string PasswordHash { get; init; }
    public string? NormalizedEmail { get; init; }
    public bool IsEmailConfirmed { get; init; }
    public bool IsEmailVerified { get; init; } 
    public bool IsPhoneConfirmed { get; init; }
    public DateTime? LastLogin { get; init; }
    public Guid InstitutionId { get; init; } 
    public InstitutionsResponse? Institutions { get; init; }
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
    public bool IsActive { get; set; }
}