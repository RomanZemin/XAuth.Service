using MassTransit;
using XAuth.Application.Interfaces;
using XAuth.Domain.Events;

namespace XAuth.Persistance.Event_Bus;

public class MassTransitEventBus : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public async Task Publish<T>(T @event, CancellationToken cancellationToken) where T : class
    {
        await _publishEndpoint.Publish(@event, cancellationToken);
    }

    public Task Publish(UserRegisteredEvent userRegisteredEvent, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}