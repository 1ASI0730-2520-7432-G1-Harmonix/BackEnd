namespace com.split.backend.IAM.Interface.REST.Resources.UserIncome;

public record UserIncomeResource(
    string Id,
    long UserId,
    string Income,
    string CreatedDate,
    string UpdatedDate);