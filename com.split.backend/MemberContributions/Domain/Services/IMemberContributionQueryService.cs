using com.split.backend.MemberContributions.Domain.Model.Aggregates;
using com.split.backend.MemberContributions.Domain.Model.Queries;

namespace com.split.backend.MemberContributions.Domain.Services;

public interface IMemberContributionQueryService
{
    Task<MemberContribution?> Handle(GetAllMemberContributionsQuery query);
    Task<MemberContribution?> Handle(GetMemberContributionsByContributionIdQuery query);
    Task<MemberContribution?> Handle(GetMemberContributionsByMemberIdQuery query);
    
}