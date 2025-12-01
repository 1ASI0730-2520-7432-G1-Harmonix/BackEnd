using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Model.Commands;
using com.split.backend.IAM.Domain.Model.Events;
using com.split.backend.IAM.Domain.Repositories;
using com.split.backend.IAM.Domain.Services;
using com.split.backend.Shared.Domain.Repositories;
using Cortex.Mediator;

namespace com.split.backend.IAM.Application.Internal.CommandServices;

public class UserIncomeCommandServices(IUserIncomeRepository userIncomeRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork, IMediator domainEventPublisher) : IUserIncomeCommandService
{
    public async Task<UserIncome?> Handle(CreateUserIncomeCommand command)
    {
        if(userRepository.FindByIdAsync((int)command.UserId) == null) 
            throw new ArgumentException("User does not exist");
        
        var userIncome = new UserIncome(command);

        if (userIncomeRepository.ExistsByUserId(command.UserId)) 
            throw new Exception("User already exists");

        await userIncomeRepository.AddAsync(userIncome);
        await unitOfWork.CompleteAsync();


        await domainEventPublisher.PublishAsync(new UserIncomeCreatedEvent(userIncome.Id));
        
        return userIncome;
    }


    public async Task<UserIncome?> Handle(UpdateUserIncomeCommand command)
    {
        var userIncome = await userIncomeRepository.FindByStringIdAsync(command.Id);
        if(userIncome == null) throw new ArgumentException("User does not exist");
        
        userIncome.Update(command);
        await unitOfWork.CompleteAsync();
        
        await domainEventPublisher.PublishAsync(new UserIncomeUpdatedEvent(userIncome.Id));
        
        return userIncome;
    }
    
    
}