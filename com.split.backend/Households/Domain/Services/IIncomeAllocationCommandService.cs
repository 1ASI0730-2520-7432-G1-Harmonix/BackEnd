using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Households.Domain.Models.Commands;

namespace com.split.backend.Households.Domain.Services;

public interface IIncomeAllocationCommandService
{
    public Task<IncomeAllocation?> Handle(CreateIncomeAllocationCommand command);

    public Task<bool> Handle(DeleteIncomeAllocationCommand command);
    
    public Task<IncomeAllocation?> Handle(UpdateIncomeAllocationCommand command);
}