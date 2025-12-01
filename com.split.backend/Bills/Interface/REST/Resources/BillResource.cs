namespace com.split.backend.Bills.Interface.REST.Resources;

public record BillResource(
    string Id,
    string HouseHoldId,
    string Description,
    decimal Amount,
    long CreatedBy,
    string PaymentDate
);