using com.split.backend.IAM.Application.Internal.OutboundServices;
using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Model.Commands;
using com.split.backend.IAM.Domain.Repositories;
using com.split.backend.IAM.Domain.Services;
using com.split.backend.IAM.Domain.Model.ValueObjects;
using IUnitOfWork = com.split.backend.Shared.Domain.Repositories.IUnitOfWork;

namespace com.split.backend.IAM.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork
    ) : IUserCommandService
{
    public async Task<(User user, string token, bool wasNewUser)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByEmailAsync(command.EmailAddress);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.Password))
            throw new Exception("Invalid email or password");

        var wasNewUser = user.IsNewUser ?? false;
        
        if (wasNewUser)
        {
            user.IsNewUser = false;
            userRepository.Update(user);
            await unitOfWork.CompleteAsync();
        }

        var token = tokenService.GenerateToken(user);
        return (user, token, wasNewUser);
    }

    public async Task Handle(SignUpCommand command)
    {
        if(userRepository.ExistsByEmail(command.EmailAddress))
            throw new Exception("Email already exists");

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command,  hashedPassword)
        {
            // Ensure onboarding flag starts true even if DB default changes
            IsNewUser = true,
            Plan = Enum.IsDefined(typeof(EPlan), command.Plan) ? (EPlan)command.Plan : EPlan.Free
        };
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<User?> Handle(UpdateUserCommand command)
    {
        var user = await userRepository.FindByEmailAsync(command.EmailAddress);
        if (user == null) return null;

        user.UpdateUsername(command.Username);
        
        userRepository.Update(user);
        
        await unitOfWork.CompleteAsync();

        return user;
    }

    public async Task<bool> Handle(DeleteUserCommand command)
    {
        var user = await userRepository.FindByEmailAsync(command.EmailAddress);
        if(user == null) return false;
        
        userRepository.Remove(user);
        await unitOfWork.CompleteAsync();

        return true;
    }
}
