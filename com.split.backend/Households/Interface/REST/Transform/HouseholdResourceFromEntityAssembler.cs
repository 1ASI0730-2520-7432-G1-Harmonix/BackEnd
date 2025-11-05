using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Households.Domain.Models.Commands;
using com.split.backend.Households.Domain.Models.ValueObjects;
using com.split.backend.Households.Interface.REST.Resources;

namespace com.split.backend.Households.Interface.REST.Transform;

public static class HouseholdResourceFromEntityAssembler
{
    public static CreateHouseholdCommand ToResourceFromEntity(HouseHold entity)
    {
        return new CreateHouseholdCommand(
            entity.Name,
            entity.RepresentativeId,
            ((int)entity.Currency).ToString());
    }
}