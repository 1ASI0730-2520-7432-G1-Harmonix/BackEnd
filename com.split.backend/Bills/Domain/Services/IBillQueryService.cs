using com.split.backend.Bills.Domain.Models.Aggregates;
using com.split.backend.Bills.Domain.Models.Queries;

namespace com.split.backend.Bills.Domain.Services;

public interface IBillQueryService
{
    Task<List<Bill>> Handle(GetBillsByHouseholdQuery query);
}