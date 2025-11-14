using com.split.backend.Bills.Domain.Models.Aggregates;

namespace com.split.backend.Bills.Domain.Repositories;

public interface IBillRepository
{
    Task AddAsync(Bill entity);
    Task<Bill?> FindByIdAsync(Guid id, string householdId);
    Task<List<Bill>> ListAsync(string householdId, DateOnly? from, DateOnly? to, string? status);
    void Remove(Bill entity);
}