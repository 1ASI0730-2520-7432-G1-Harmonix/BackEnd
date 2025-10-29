namespace com.split.backend.IAM.Interface.REST.Resources;

public record AuthenticatedUserResource(int Id, string Email, string Token);