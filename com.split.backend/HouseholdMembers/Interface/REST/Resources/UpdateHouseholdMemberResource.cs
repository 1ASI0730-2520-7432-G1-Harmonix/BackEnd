namespace com.split.backend.HouseholdMembers.Interface.REST.Resources;

public record UpdateHouseholdMemberResource(
    string HouseholdId,
    int UserId,
    bool IsRepresentative
);

