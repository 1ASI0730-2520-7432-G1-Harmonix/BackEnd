namespace com.split.backend.IAM.Interface.OutboundServices.ACL;

public interface IExternalHouseHoldService
{
    Task<string> CreateHousehold(string firstName, string lastName, string email,
        string street, string number, string city,
        string postalCode, string country);
    
    Task<string> FetchHouseHoldById(string id);

}