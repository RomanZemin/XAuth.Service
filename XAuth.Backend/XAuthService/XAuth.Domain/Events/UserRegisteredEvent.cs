using XAuth.Domain.Interfaces;

namespace XAuth.Domain.Events;

public record UserRegisteredEvent(Guid UserId, string Email, string NormalizedEmail) : IDomainEvent;