using EventBus.Base.Events;
using Newtonsoft.Json;

namespace RabbitListener.Application.IntegrationEvents.Events
{
    public class urlsIntegrationEvent : IntegrationEvent
    {
        [JsonProperty]
        public string Url { get; set; }

        [JsonProperty]
        public string ServiceName { get; set; }

        [Newtonsoft.Json.JsonConstructor]
        public urlsIntegrationEvent(string url, string serviceName = "RabbitListener")
        {
            Url = url;
            ServiceName = serviceName;
        }

    }
}
