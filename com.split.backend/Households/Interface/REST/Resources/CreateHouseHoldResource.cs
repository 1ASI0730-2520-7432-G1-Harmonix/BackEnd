namespace com.split.backend.Households.Interface.REST.Resources;

public record CreateHouseHoldResource(
    string Name, long RepresentativeId ,string Currency);