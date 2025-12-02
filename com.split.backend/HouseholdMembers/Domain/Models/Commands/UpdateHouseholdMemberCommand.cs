namespace com.split.backend.HouseholdMembers.Domain.Models.Commands;

public record UpdateHouseholdMemberCommand(
    int Id,
    string HouseholdId,
    int UserId,
    bool IsRepresentative
);
