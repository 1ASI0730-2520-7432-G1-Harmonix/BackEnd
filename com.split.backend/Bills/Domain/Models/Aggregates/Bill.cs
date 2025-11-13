namespace com.split.backend.Bills.Domain.Models.Aggregates;

public enum EBillStatus { Pending = 0, Paid = 1, Overdue = 2, Canceled = 3 }

public class Bill 
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string HouseholdId { get; set; } = string.Empty;
    public int CreatedByUserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = "General";
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "PEN";
    public DateOnly DueDate { get; set; }
    public DateTime? PaidAt { get; set; }
    public DateTime? Overdue { get; set; }
    public DateTime? Canceled { get; set; }
    public EBillStatus Status { get; set; } = EBillStatus.Pending;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}