using EventBus.Base.Events;

namespace RabbitListener.Application.IntegrationEvents.Events
{
    public class UrlListenerCreatedIntegrationEvent : IntegrationEvent
    {
        public string UrlAddress { get; set; }

    }
}
