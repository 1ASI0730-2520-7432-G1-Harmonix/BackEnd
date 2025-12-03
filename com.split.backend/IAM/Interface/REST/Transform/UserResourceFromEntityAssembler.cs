using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Interface.REST.Resources;

namespace com.split.backend.IAM.Interface.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity)
    {
        return new UserResource(
            entity.Id,
            entity.Email.Address,
            entity.PersonName.FirstName,
            entity.HouseholdId,
            entity.Role.ToString(),
            entity.Plan?.ToString() ?? "Unspecified",
            entity.Photo?.AbsoluteUri ?? string.Empty,
            entity.ProfileLockedUntil?.ToString() ?? string.Empty,
            entity.IsNewUser?.ToString() ?? "false"
        );
    }
}