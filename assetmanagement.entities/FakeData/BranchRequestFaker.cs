using AssetManagement.Entities.DTOs.Requests;
using Bogus;

namespace AssetManagement.Entities.FakeData;

public static class BranchRequestFaker
{
    public static Faker<BranchesCreateRequest> GetCreateRequestFaker(Guid institutionId)
    {
        return new Faker<BranchesCreateRequest>()
            .RuleFor(x => x.Id, _ => Guid.NewGuid())
            .RuleFor(x => x.BranchName, f => f.Company.CompanyName())
            .RuleFor(x => x.Latitude, f => f.Address.Latitude())
            .RuleFor(x => x.Longitude, f => f.Address.Longitude())
            .RuleFor(x => x.CreatedAt, _ => DateTime.UtcNow)
            .RuleFor(x => x.UpdatedAt, _ => DateTime.UtcNow)
            .RuleFor(x => x.IsActive, _ => true)
            .RuleFor(x => x.IsHeadOffice, _ => false)
            .RuleFor(x => x.InstitutionId, _ => institutionId);
    }
}