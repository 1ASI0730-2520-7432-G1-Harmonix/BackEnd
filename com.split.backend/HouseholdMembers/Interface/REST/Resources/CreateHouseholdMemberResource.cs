namespace com.split.backend.HouseholdMembers.Interface.REST.Resources;

public record CreateHouseholdMemberResource(
    string HouseholdId, 
    int UserId,
    bool IsRepresentative
);

