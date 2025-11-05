namespace com.split.backend.Households.Interface.REST.Resources;

public record HouseHoldResource(string Id, string Name, 
    long RepresentativeId, string Currency);