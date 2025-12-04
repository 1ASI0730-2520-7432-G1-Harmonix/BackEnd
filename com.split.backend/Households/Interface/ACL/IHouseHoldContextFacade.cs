namespace com.split.backend.Households.Interface.ACL;

public interface IHouseHoldContextFacade
{
    Task<string> CreateHouseHold(
        string name, long representativeId, string currency, string description,
        int memberCount);
    
    Task<string> FetchHouseHoldById(string householdId);
    
    Task<bool> ExistsById(string householdId); 
}