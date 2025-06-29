namespace XAuth.Contracts.Events;

public record UserCreatedIntegrationEvent(
    Guid UserId,
    string Email,
    string NormalizedEmail,
    DateTime CreatedAt);