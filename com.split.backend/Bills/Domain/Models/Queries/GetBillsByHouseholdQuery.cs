namespace com.split.backend.Bills.Domain.Models.Queries;

public record GetBillsByHouseholdQuery(string HouseholdId, DateOnly? From, DateOnly? To, string? Status);
