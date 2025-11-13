using com.split.backend.Bills.Domain.Models.Aggregates;
using com.split.backend.Bills.Domain.Models.Commands;
using com.split.backend.Bills.Interface.REST.Resources;

namespace com.split.backend.Bills.Interface.REST.Transform;

public static class BillResourceAssemblers
{
    public static CreateBillCommand ToCommand(string householdId, int userId, CreateBillResource r) =>
        new(householdId, userId, r.Title, r.Category, r.Amount, r.Currency, r.DueDate, r.Notes);
    
    public static UpdateBillCommand ToCommand(Guid id, string householdId, UpdateBillResource r) =>
        new(id, householdId, r.Title, r.Category, r.Amount, r.Currency, r.DueDate, r.Status, r.Notes);

    public static BillResource ToResource(Bill b) => new(
        b.Id, b.HouseholdId, b.CreatedByUserId, b.Title, b.Category, b.Amount, b.Currency,
        b.DueDate, b.PaidAt, b.Status.ToString(), b.Notes, b.CreatedAt, b.UpdatedAt
    );

}