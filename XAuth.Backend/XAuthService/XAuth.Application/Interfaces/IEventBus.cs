using XAuth.Domain.Events;

namespace XAuth.Application.Interfaces;

public interface IEventBus
{
    Task Publish(UserRegisteredEvent userRegisteredEvent, CancellationToken cancellationToken);
}