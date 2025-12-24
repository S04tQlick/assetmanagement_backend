using AssetManagement.Entities.DTOs.Requests;
using Bogus;

namespace AssetManagement.Entities.FakeData;

public abstract class UserCreateRequestFaker
{
    public static Faker<UsersCreateRequest> GetCreateRequestFaker(Guid institutionId)
    {
        return new Faker<UsersCreateRequest>()
            .RuleFor(u => u.Id, _ => Guid.NewGuid())
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.InstitutionId, _ => institutionId)
            .RuleFor(u => u.EmailAddress, f => f.Internet.Email())
            .RuleFor(u => u.PasswordHash, f => f.Internet.Password(12, true)) 
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber("+44##########"))
            .RuleFor(u => u.CreatedAt, _ => DateTime.UtcNow)
            .RuleFor(u => u.UpdatedAt, _ => DateTime.UtcNow)
            .RuleFor(u => u.IsActive, _ => true);
    }
}