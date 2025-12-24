using AssetManagement.Entities.DTOs.Requests;
using Bogus;

namespace AssetManagement.Entities.FakeData;

public static class InstitutionRequestFaker
{
    public static Faker<InstitutionsCreateRequest> GetCreateRequestFaker()
    {
        return new Faker<InstitutionsCreateRequest>()
            .RuleFor(r => r.Id,_ => Guid.NewGuid())
            .RuleFor(i => i.InstitutionName, f => f.Company.CompanyName())
            .RuleFor(i => i.InstitutionEmail, f => f.Internet.Email())
            .RuleFor(i => i.InstitutionContactNumber, f => f.Phone.PhoneNumber("+44##########"))
            .RuleFor(i => i.PrimaryColor, f => f.Internet.Color().Replace("#", "#"))
            .RuleFor(i => i.SecondaryColor, f => f.Internet.Color().Replace("#", "#"))
            .RuleFor(i => i.LogoSanityId, f => f.Random.Guid().ToString())
            .RuleFor(i => i.LogoUrl, f => f.Image.PicsumUrl())
            .RuleFor(r => r.CreatedAt, _ => DateTime.UtcNow)
            .RuleFor(r => r.UpdatedAt, _ => DateTime.UtcNow)
            .RuleFor(r => r.IsActive, _ => true);
    }
}