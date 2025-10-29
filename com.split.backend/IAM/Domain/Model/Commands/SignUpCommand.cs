namespace com.split.backend.IAM.Domain.Model.Commands;

public record SignUpCommand(string UserName,string EmailAddress, string Password,string Role);