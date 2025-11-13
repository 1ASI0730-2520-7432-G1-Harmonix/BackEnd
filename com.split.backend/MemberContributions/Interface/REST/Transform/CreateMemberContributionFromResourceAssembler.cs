using com.split.backend.MemberContributions.Domain.Model.Aggregates;
using com.split.backend.MemberContributions.Domain.Model.Commands;
using com.split.backend.MemberContributions.Interface.REST.Resources;

namespace com.split.backend.MemberContributions.Interface.REST.Transform;

public static class CreateMemberContributionFromResourceAssembler
{
    public static CreateMemberContributionCommand ToCommandFromResource(MemberContribution entity)
    {
        return new CreateMemberContributionCommand(entity.ContributionId, entity.MemberId, entity.Amount);
    }
    
}