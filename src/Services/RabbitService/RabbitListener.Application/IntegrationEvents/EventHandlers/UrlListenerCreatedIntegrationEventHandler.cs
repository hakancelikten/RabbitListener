using EventBus.Base.Abstraction;
using RabbitListener.Application.IntegrationEvents.Events;

namespace RabbitListener.Application.IntegrationEvents.EventHandlers
{
    public class UrlListenerCreatedIntegrationEventHandler : IIntegrationEventHandler<UrlListenerCreatedIntegrationEvent>
    {
        public Task Handle(UrlListenerCreatedIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }

}
