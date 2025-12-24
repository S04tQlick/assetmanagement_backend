using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.Enums;
using Bogus;

namespace AssetManagement.Entities.FakeData;

public static class RolesRequestFaker
{
    public static Faker<RolesCreateRequest> GetCreateRequestFaker()
    {
        return new Faker<RolesCreateRequest>()
            .RuleFor(r => r.Id, _ => Guid.NewGuid())
            .RuleFor(r => r.RoleName, f => f.PickRandom(Enum.GetNames<UserRolesEnum>()))
            .RuleFor(r => r.CreatedAt, _ => DateTime.UtcNow)
            .RuleFor(r => r.UpdatedAt, _ => DateTime.UtcNow)
            .RuleFor(r => r.IsActive, _ => true);
    }
}