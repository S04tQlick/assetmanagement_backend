using AssetManagement.Entities.DTOs.Requests;
using Bogus;

namespace AssetManagement.Entities.FakeData;

public abstract class VendorCreateRequestFaker
{
    public static Faker<VendorsCreateRequest> GetCreateRequestFaker(Guid institutionId)
    {
        return new Faker<VendorsCreateRequest>().RuleFor(r => r.Id, _ => Guid.NewGuid())
            .RuleFor(r => r.VendorsName, f => f.Company.CompanyName())
            .RuleFor(v => v.EmailAddress, f => f.Internet.Email())
            .RuleFor(v => v.ContactInfo, f => f.Phone.PhoneNumber())
            .RuleFor(r => r.InstitutionId, _ => institutionId)
            .RuleFor(r => r.CreatedAt, _ => DateTime.UtcNow)
            .RuleFor(r => r.UpdatedAt, _ => DateTime.UtcNow)
            .RuleFor(r => r.IsActive, _ => true);
    }
}