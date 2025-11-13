using com.split.backend.Bills.Domain.Models.Aggregates;

namespace com.split.backend.Bills.Interface.REST.Resources;

public record UpdateBillResource(

    string? Title,
    string? Category,
    decimal? Amount,
    string? Currency,
    DateOnly? DueDate,
    EBillStatus? Status,
    string? Notes
    
);