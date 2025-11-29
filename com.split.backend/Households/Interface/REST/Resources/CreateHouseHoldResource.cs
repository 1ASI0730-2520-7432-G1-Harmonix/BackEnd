namespace com.split.backend.Households.Interface.REST.Resources;

public record CreateHouseHoldResource(
    string Name, long RepresentativeId ,string Currency, string Description, int MemberCount);