using AssetManagement.Entities.DTOs.Requests;
using Bogus;

namespace AssetManagement.Entities.FakeData;

public abstract class UserRoleRequestFaker
{
    public static Faker<UserRolesCreateRequest> GetCreateRequestFaker(Guid userId, Guid roleId)
    {
        return new Faker<UserRolesCreateRequest>().RuleFor(r => r.Id, _ => Guid.NewGuid())
            .RuleFor(r => r.UserId, _ => userId)
            .RuleFor(r => r.RoleId, _ => roleId)
            .RuleFor(r => r.IsActive, _ => true)
            .RuleFor(r => r.CreatedAt, _ => DateTime.UtcNow)
            .RuleFor(r => r.UpdatedAt, _ => DateTime.UtcNow);
    }
}