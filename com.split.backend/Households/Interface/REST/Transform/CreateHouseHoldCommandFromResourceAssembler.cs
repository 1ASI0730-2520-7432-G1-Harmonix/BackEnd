using com.split.backend.Households.Domain.Models.Commands;
using com.split.backend.Households.Interface.REST.Resources;

namespace com.split.backend.Households.Interface.REST.Transform;

public static class CreateHouseHoldCommandFromResourceAssembler
{
    public static CreateHouseholdCommand ToCommandFromResource(CreateHouseHoldResource resource)
    {
        if (resource is null)
        {
            throw new System.ArgumentNullException(nameof(resource));
        }
        
        return new CreateHouseholdCommand(
            resource.Name,
            resource.RepresentativeId,
            resource.Currency);
    }
    
}