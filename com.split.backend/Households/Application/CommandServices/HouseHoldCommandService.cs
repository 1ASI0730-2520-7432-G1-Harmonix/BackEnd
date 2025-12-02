using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Households.Domain.Models.Commands;
using com.split.backend.Households.Domain.Repositories;
using com.split.backend.Households.Domain.Services;
using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Repositories;
using com.split.backend.Shared.Domain.Repositories;

namespace com.split.backend.Households.Application.CommandServices;

public class HouseHoldCommandService(
    IHouseHoldRepository houseHoldRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
    ) : IHouseHoldCommandService
{
    public async Task<HouseHold?> Handle(CreateHouseholdCommand command)
    {
        // Validate representative exists
        var rep = await userRepository.FindByIdAsync((int)command.RepresentativeId);
        if (rep is null) return null;

        var household = new HouseHold(command);
        try
        {
            await houseHoldRepository.AddAsync(household);
            await unitOfWork.CompleteAsync();
            return household;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    public async Task<HouseHold?> Handle(UpdateHouseHoldCommand command)
    {
        var household = await houseHoldRepository.FindByStringIdAsync(command.Id);
        if (household == null) return null;

        household.UpdateHouseHold(command);
        
        houseHoldRepository.Update(household);
        
        await unitOfWork.CompleteAsync();

        return household;
    }


    

    public async Task<bool> Handle(DeleteHouseHoldCommand command)
    {
        var household = await houseHoldRepository.FindByStringIdAsync(command.Id);
        if (household == null) return false;
        
        houseHoldRepository.Remove(household);
        await unitOfWork.CompleteAsync();

        return true;
    }
    
    
}
