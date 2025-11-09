using com.split.backend.HouseholdMembers.Domain.Models.Aggregates;
using com.split.backend.HouseholdMembers.Domain.Models.Commands;
using com.split.backend.HouseholdMembers.Domain.Repositories;
using com.split.backend.HouseholdMembers.Domain.Services;
using com.split.backend.HouseholdMembers.Interface.ACL;
using IUnitOfWork = com.split.backend.Shared.Domain.Repositories.IUnitOfWork;

namespace com.split.backend.HouseholdMembers.Application.Internal.CommandServices;

public class HouseholdMemberCommandService(
    IHouseholdMemberRepository repository,
    IHouseholdContextFacade householdContextFacade,
    IUserContextFacade userContextFacade,
    IUnitOfWork unitOfWork
) : IHouseholdMemberCommandService
{
    public async Task<HouseholdMember?> Handle(CreateHouseholdMemberCommand command)
    {
        // Validar que el household existe
        if (!await householdContextFacade.ExistsHouseholdByIdAsync(command.HouseholdId))
            throw new Exception("Household not found");

        // Validar que el usuario existe
        if (!await userContextFacade.ExistsUserByIdAsync(command.UserId))
            throw new Exception("User not found");

        // Validar que no existe ya el miembro
        if (repository.ExistsByHouseholdIdAndUserId(command.HouseholdId, command.UserId))
            throw new Exception("Member already exists in this household");

        // Si se está creando como representante, validar que no haya otro representante
        if (command.IsRepresentative)
        {
            var existingRepresentative = await repository.FindRepresentativeByHouseholdIdAsync(command.HouseholdId);
            if (existingRepresentative != null)
                throw new Exception("A representative already exists for this household");
        }

        var member = new HouseholdMember(command.HouseholdId, command.UserId, command.IsRepresentative);
        
        try
        {
            await repository.AddAsync(member);
            await unitOfWork.CompleteAsync();
            return member;
        }
        catch (Exception e)
        {
            throw new Exception($"Error creating household member: {e.Message}");
        }
    }

    public async Task<bool> Handle(DeleteHouseholdMemberCommand command)
    {
        var member = await repository.FindByIdAsync(command.Id);
        if (member == null) return false;

        repository.Remove(member);
        await unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<HouseholdMember?> Handle(UpdateHouseholdMemberCommand command)
    {
        var member = await repository.FindByIdAsync(command.Id);
        if (member == null) return null;

        // Si se está promoviendo a representante, validar que no haya otro
        if (command.IsRepresentative && !member.IsRepresentative)
        {
            var existingRepresentative = await repository.FindRepresentativeByHouseholdIdAsync(member.HouseholdId);
            if (existingRepresentative != null && existingRepresentative.Id != member.Id)
                throw new Exception("A representative already exists for this household");
        }

        member.IsRepresentative = command.IsRepresentative;
        
        repository.Update(member);
        await unitOfWork.CompleteAsync();
        return member;
    }

    public async Task<HouseholdMember?> Handle(PromoteToRepresentativeCommand command)
    {
        var member = await repository.FindByIdAsync(command.Id);
        if (member == null) return null;

        // Validar que no haya otro representante
        var existingRepresentative = await repository.FindRepresentativeByHouseholdIdAsync(member.HouseholdId);
        if (existingRepresentative != null && existingRepresentative.Id != member.Id)
            throw new Exception("A representative already exists for this household");

        member.PromoteToRepresentative();
        
        repository.Update(member);
        await unitOfWork.CompleteAsync();
        return member;
    }

    public async Task<HouseholdMember?> Handle(DemoteRepresentativeCommand command)
    {
        var member = await repository.FindByIdAsync(command.Id);
        if (member == null) return null;

        if (!member.IsRepresentative)
            throw new Exception("Member is not a representative");

        member.DemoteRepresentative();
        
        repository.Update(member);
        await unitOfWork.CompleteAsync();
        return member;
    }
}

