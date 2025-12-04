using com.split.backend.Households.Domain.Models.Commands;
using com.split.backend.Households.Domain.Models.Queries;
using com.split.backend.Households.Domain.Services;
using com.split.backend.Households.Interface.ACL;

namespace com.split.backend.Households.Application.ACL;

public class HouseholdsContextFacade(
    IHouseHoldCommandService commandService,
    IHouseHoldQueryService queryService
    ) : IHouseHoldContextFacade
{

    public async Task<string> CreateHouseHold(string name, long representativeId, string currency, string description,
        int memberCount)
    {
        var createHouseHoldCommand = new CreateHouseholdCommand(name, representativeId, currency, description, memberCount);
        var houseHold = await commandService.Handle(createHouseHoldCommand);
        return houseHold?.Id ?? String.Empty;
    }

    public async Task<string> FetchHouseHoldById(string id)
    {
        var getHouseHoldByIdQuery = new GetHouseHoldByIdQuery(id);
        var houseHold = await queryService.Handle(getHouseHoldByIdQuery);
        return houseHold?.Id ?? String.Empty;
    }

    public async Task<bool> ExistsById(string householdId)
    {
        var getHouseHoldByIdQuery = new GetHouseHoldByIdQuery(householdId);
        if(await queryService.Handle(getHouseHoldByIdQuery) == null) return false;
        return true;
    }
}