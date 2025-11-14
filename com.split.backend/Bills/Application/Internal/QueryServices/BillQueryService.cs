namespace com.split.backend.Bills.Application.Internal.QueryServices;
using com.split.backend.Bills.Domain.Models.Aggregates;
using com.split.backend.Bills.Domain.Models.Queries;
using com.split.backend.Bills.Domain.Repositories;
using com.split.backend.Bills.Domain.Services;

public class BillQueryService(IBillRepository repo) : IBillQueryService
{
    public Task<List<Bill>> Handle(GetBillsByHouseholdQuery q) =>
        repo.ListAsync(q.HouseholdId, q.From, q.To, q.Status);
}