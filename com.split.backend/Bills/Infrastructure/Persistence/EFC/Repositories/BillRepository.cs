using com.split.backend.Bills.Domain.Models.Aggregates;
using com.split.backend.Bills.Domain.Repositories;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.Bills.Infrastructure.Persistence.EFC.Repositories;

public class BillRepository(AppDbContext context) : IBillRepository
{
    public async Task AddAsync(Bill entity) => await context.Set<Bill>().AddAsync(entity);
    
    public async Task<Bill?> FindByIdAsync(Guid id, string householdId)
        => await context.Set<Bill>()
            .FirstOrDefaultAsync(b => b.Id == id && b.HouseholdId == householdId);

    public async Task<List<Bill>> ListAsync(string householdId, DateOnly? from, DateOnly? to, string? status)
    {
        var query = context.Set<Bill>().AsQueryable().Where(b => b.HouseholdId == householdId);
        
        if(from is not null) query = query.Where(b => b.DueDate >= from);
        if(to is not null) query = query.Where(b => b.DueDate <= to);

        if (!string.IsNullOrWhiteSpace(status) &&
            Enum.TryParse<EBillStatus>(status, true, out var parsed))
        {
            query = query.Where( b => b.Status == parsed);
        }
        
        return await query.OrderBy(b => b.DueDate).ToListAsync();
    }
    
    public void Remove(Bill entity) => context.Set<Bill>().Remove(entity);
}