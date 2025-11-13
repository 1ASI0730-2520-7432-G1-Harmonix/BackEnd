using com.split.backend.Bills.Domain.Models.Aggregates;
using com.split.backend.Bills.Domain.Models.Commands;
using com.split.backend.Bills.Domain.Repositories;
using com.split.backend.Bills.Domain.Services;
using com.split.backend.Shared.Domain.Repositories;

namespace com.split.backend.Bills.Application.Internal.CommandServices;

public class BillCommandService(IBillRepository repo, IUnitOfWork uow) : IBillCommandService
{
    public async Task<Bill?> Handle(CreateBillCommand c)
    {
        var bill = new Bill
        {
            HouseholdId = c.HouseholdId,
            CreatedByUserId = c.CreatedByUserId,
            Title = c.Title,
            Category = c.Category,
            Amount = c.Amount,
            Currency = c.Currency,
            DueDate = c.DueDate,
            Notes = c.Notes
        };

        await repo.AddAsync(bill);
        await uow.CompleteAsync();
        return bill;
    }

    public async Task<Bill?> Handle(UpdateBillCommand c)
    {
        var bill = await repo.FindByIdAsync(c.Id, c.HouseholdId);
        if (bill is null) return null;

        bill.Title    = c.Title    ?? bill.Title;
        bill.Category = c.Category ?? bill.Category;
        bill.Amount   = c.Amount   ?? bill.Amount;
        bill.Currency = c.Currency ?? bill.Currency;
        bill.DueDate  = c.DueDate  ?? bill.DueDate;
        bill.Status   = c.Status   ?? bill.Status;
        bill.Notes    = c.Notes    ?? bill.Notes;

        bill.PaidAt    = bill.Status == EBillStatus.Paid ? bill.PaidAt ?? DateTime.UtcNow : null;
        bill.Overdue   = bill.Status == EBillStatus.Overdue ? bill.Overdue ?? DateTime.UtcNow : null;
        bill.Canceled = bill.Status == EBillStatus.Canceled ? bill.Canceled ?? DateTime.UtcNow : null;
        bill.UpdatedAt = DateTime.UtcNow;

        await uow.CompleteAsync();
        return bill;
    }

    public async Task<bool> Handle(DeleteBillCommand c)
    {
        var bill = await repo.FindByIdAsync(c.Id, c.HouseholdId);
        if (bill is null) return false;

        repo.Remove(bill);
        await uow.CompleteAsync();
        return true;
    }
}