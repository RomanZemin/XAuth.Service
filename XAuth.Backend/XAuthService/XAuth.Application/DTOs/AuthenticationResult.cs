namespace XAuth.Application.DTOs;

public record AuthenticationResult(Guid UserId, string Token);