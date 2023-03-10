using EventBus.Base.Events;

namespace RabbitListener.Application.IntegrationEvents.Events
{
    public class urlsIntegrationEvent : IntegrationEvent
    {
        public string Url { get; set; }
        public string ServiceName { get; set; }
        public urlsIntegrationEvent(string url, string serviceName = "RabbitListener")
        {
            Url = url;
            ServiceName = serviceName;
        }

    }
}
