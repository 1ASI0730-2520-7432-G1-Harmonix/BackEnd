namespace com.split.backend.Bills.Domain.Models.Commands;
public record DeleteBillCommand(Guid Id, string HouseholdId);