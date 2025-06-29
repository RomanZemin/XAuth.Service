using XAuth.Domain.Interfaces;

namespace XAuth.Domain.Events;

public record EmailConfirmedEvent(Guid UserId, string Email) : IDomainEvent;