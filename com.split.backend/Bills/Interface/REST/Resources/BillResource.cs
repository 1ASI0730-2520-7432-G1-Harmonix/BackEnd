namespace com.split.backend.Bills.Interface.REST.Resources;

public record BillResource(
    Guid Id,
    string HouseholdId,
    int CreatedByUserId,
    string Title,
    string Category,
    decimal Amount,
    string Currency,
    DateOnly DueDate,
    DateTime? PaidAt,
    string Status,
    string? Notes,
    DateTime CreatedAt,
    DateTime UpdatedAt

);