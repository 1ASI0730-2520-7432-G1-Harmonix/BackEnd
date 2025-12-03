using com.split.backend.IAM.Application.Internal.OutboundServices;
using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Model.Commands;
using com.split.backend.IAM.Domain.Repositories;
using com.split.backend.IAM.Domain.Services;
using com.split.backend.IAM.Domain.Model.ValueObjects;
using com.split.backend.Invitations.Domain.Repositories;
using com.split.backend.Invitations.Domain.Models.Aggregates;
using com.split.backend.HouseholdMembers.Domain.Repositories;
using com.split.backend.Households.Domain.Repositories;
using Role = com.split.backend.Households.Domain.Models.ValueObjects.Role;
using IUnitOfWork = com.split.backend.Shared.Domain.Repositories.IUnitOfWork;

namespace com.split.backend.IAM.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IInvitationRepository invitationRepository,
    IHouseholdMemberRepository householdMemberRepository,
    IHouseHoldRepository houseHoldRepository,
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
        var role = Enum.Parse<Role>(command.Role);

        if (role == Role.Member && !string.IsNullOrWhiteSpace(command.HouseholdId))
        {
            var invitation = await invitationRepository.FindPendingAsync(command.EmailAddress, command.HouseholdId);
            if (invitation == null || invitation.ExpiresAt < DateTime.UtcNow)
                throw new Exception("Invitation not found or expired.");

            // validate household exists and capacity
            var household = await houseHoldRepository.FindByStringIdAsync(command.HouseholdId);
            if (household == null) throw new Exception("Household not found.");
            var currentMembers = await householdMemberRepository.FindByHouseholdIdAsync(command.HouseholdId);
            var count = currentMembers.Count();
            if (household.MemberCount > 0 && count >= household.MemberCount)
                throw new Exception("Household member limit reached.");

            var user = new User(command,  hashedPassword)
            {
                IsNewUser = true,
                Plan = Enum.IsDefined(typeof(EPlan), command.Plan) ? (EPlan)command.Plan : EPlan.Free
            };

            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();

            var member = new HouseholdMembers.Domain.Models.Aggregates.HouseholdMember(command.HouseholdId, user.Id, false);
            await householdMemberRepository.AddAsync(member);
            invitation.Status = InvitationStatus.Accepted;
            invitation.UpdatedDate = DateTime.UtcNow;
            invitationRepository.Update(invitation);
            await unitOfWork.CompleteAsync();
            return;
        }

        var newUser = new User(command,  hashedPassword)
        {
            IsNewUser = true,
            Plan = Enum.IsDefined(typeof(EPlan), command.Plan) ? (EPlan)command.Plan : EPlan.Free
        };
        await userRepository.AddAsync(newUser);
        await unitOfWork.CompleteAsync();
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
