namespace XAuth.Contracts.Events;

public record UserEmailConfirmedIntegrationEvent(
    Guid UserId,
    string Email,
    DateTime ConfirmedAt);