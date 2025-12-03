using com.split.backend.Invitations.Domain.Models.Aggregates;
using com.split.backend.Invitations.Domain.Models.Commands;
using com.split.backend.Invitations.Domain.Repositories;
using com.split.backend.Invitations.Domain.Services;
using com.split.backend.Shared.Domain.Repositories;

namespace com.split.backend.Invitations.Application.Internal.CommandServices;

public class InvitationCommandService(
    IInvitationRepository invitationRepository,
    IUnitOfWork unitOfWork) : IInvitationCommandService
{
    public async Task<Invitation> Handle(CreateInvitationCommand command)
    {
        var email = command.Email.Trim().ToLowerInvariant();
        var householdId = command.HouseholdId.Trim();

        if (await invitationRepository.ExistsPendingAsync(email, householdId))
            throw new Exception("A pending invitation already exists for this email and household.");

        var invitation = new Invitation
        {
            Email = email,
            HouseholdId = householdId,
            Description = command.Description ?? string.Empty
        };

        await invitationRepository.AddAsync(invitation);
        await unitOfWork.CompleteAsync();
        return invitation;
    }
}
