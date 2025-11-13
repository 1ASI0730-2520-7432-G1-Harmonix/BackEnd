namespace com.split.backend.Bills.Domain.Models.Commands;

public record CreateBillCommand(
    string HouseholdId,
    int CreatedByUserId,
    string Title,
    string Category,
    decimal Amount,
    string Currency,
    DateOnly DueDate,
    string? Notes
);