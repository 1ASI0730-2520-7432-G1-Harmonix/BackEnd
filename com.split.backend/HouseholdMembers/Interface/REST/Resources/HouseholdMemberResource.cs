namespace com.split.backend.HouseholdMembers.Interface.REST.Resources;

public record HouseholdMemberResource(
    int Id,
    string HouseholdId,
    int UserId,
    bool IsRepresentative,
    DateTime JoinedAt
);
