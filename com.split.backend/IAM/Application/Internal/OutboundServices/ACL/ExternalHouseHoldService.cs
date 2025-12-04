using com.split.backend.IAM.Interface.OutboundServices.ACL;

namespace com.split.backend.IAM.Application.Internal.OutboundServices.ACL;

public class ExternalHouseHoldService : IExternalHouseHoldService
{
    private readonly HttpClient httpClient;

    public ExternalHouseHoldService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<string> CreateHousehold(string firstName, string lastName, string email,
        string street, string number, string city,
        string postalCode, string country)
    {
        var response = await httpClient.PostAsJsonAsync("/api/v1/house_hold", new
        {
            firstName, lastName, email, street, number, city, postalCode, country
        });
        
        var result = await response.Content.ReadFromJsonAsync<CreateHouseHoldResponse>();
        return result?.id ?? string.Empty;
    }
    
    public async Task<string> FetchHouseHoldById(string id)
    {
        
        var response = await httpClient.GetAsync($"api/v1/house_hold/{id}");
        var result = await response.Content.ReadFromJsonAsync<CreateHouseHoldResponse>();
        return result?.id ?? string.Empty;
        
    }
}