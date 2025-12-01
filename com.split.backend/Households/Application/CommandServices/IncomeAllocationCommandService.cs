using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Households.Domain.Models.Commands;
using com.split.backend.Households.Domain.Repositories;
using com.split.backend.Households.Domain.Services;
using com.split.backend.Shared.Domain.Repositories;

namespace com.split.backend.Households.Application.CommandServices;

public class IncomeAllocationCommandService(
    IIncomeAllocationRepository incomeAllocationRepository,
    IHouseHoldRepository houseHoldRepository,
    IUnitOfWork unitOfWork) : IIncomeAllocationCommandService
{
    public async Task<IncomeAllocation?> Handle(CreateIncomeAllocationCommand command)
    {
        if(!houseHoldRepository.ExistsById(command.HouseholdId))
            throw new KeyNotFoundException("Household does not exist");
        
        var incomeAllocation = new IncomeAllocation(command);
        try
        {
            await incomeAllocationRepository.AddAsync(incomeAllocation);
            await unitOfWork.CompleteAsync();
            return incomeAllocation;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<IncomeAllocation?> Handle(UpdateIncomeAllocationCommand command)
    {
        var incomeAllocation = await incomeAllocationRepository.FindByStringIdAsync(command.Id);
        if (incomeAllocation == null) return null;

        incomeAllocation.Update(command);

        await unitOfWork.CompleteAsync();
        
        return incomeAllocation;
    }

    public async Task<bool> Handle(DeleteIncomeAllocationCommand command)
    {
        var incomeAllocation = await incomeAllocationRepository.FindByStringIdAsync(command.Id);
        if(incomeAllocation == null) return false;
        
        incomeAllocationRepository.Remove(incomeAllocation);
        await unitOfWork.CompleteAsync();

        return true;
    }
    
    
}