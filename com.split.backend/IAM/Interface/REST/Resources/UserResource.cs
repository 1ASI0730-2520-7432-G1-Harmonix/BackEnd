namespace com.split.backend.IAM.Interface.REST.Resources;

public record UserResource(int Id, string Email, string PersonName, 
    string HouseHoldId, string Role);