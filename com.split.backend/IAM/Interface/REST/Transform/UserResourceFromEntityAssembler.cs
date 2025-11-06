using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Interface.REST.Resources;

namespace com.split.backend.IAM.Interface.REST.Transform;

public class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity)
    {
        return new UserResource(entity.Id,
            entity.Email.Address,
            entity.PersonName.FirstName.ToString(),
            entity.HouseholdId.ToString(),
                entity.Role.ToString());
    }
}