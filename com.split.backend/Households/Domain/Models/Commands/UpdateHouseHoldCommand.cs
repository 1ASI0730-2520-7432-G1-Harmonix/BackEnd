namespace com.split.backend.Households.Domain.Models.Commands;

public record UpdateHouseHoldCommand(string Id, string Name, long RepresentativeId, string Currency);