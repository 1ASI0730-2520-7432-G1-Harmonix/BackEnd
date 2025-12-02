using com.split.backend.HouseholdMembers.Domain.Models.Commands;
using com.split.backend.HouseholdMembers.Interface.REST.Resources;

namespace com.split.backend.HouseholdMembers.Interface.REST.Transform;

public static class UpdateHouseholdMemberCommandFromResourceAssembler
{
    public static UpdateHouseholdMemberCommand ToCommandFromResource(int id, UpdateHouseholdMemberResource resource)
    {
        if (resource is null)
        {
            throw new ArgumentNullException(nameof(resource));
        }
        
        return new UpdateHouseholdMemberCommand(
            id,
            resource.HouseholdId,
            resource.UserId,
            resource.IsRepresentative);
    }
}
