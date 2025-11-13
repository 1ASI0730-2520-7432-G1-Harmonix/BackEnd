using com.split.backend.Bills.Domain.Models.Aggregates;

namespace com.split.backend.Bills.Domain.Models.Commands;

public record UpdateBillCommand(
    Guid Id,
    string HouseholdId,
    string? Title,
    string? Category,
    decimal? Amount,
    string? Currency,
    DateOnly? DueDate,
    EBillStatus? Status,
    string? Notes
);