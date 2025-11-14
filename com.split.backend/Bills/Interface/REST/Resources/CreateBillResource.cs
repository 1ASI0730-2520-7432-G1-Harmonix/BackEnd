namespace com.split.backend.Bills.Interface.REST.Resources;

public record CreateBillResource(
    string Title,
    string Category,
    decimal Amount,
    string Currency,
    DateOnly DueDate,
    string? Notes

);