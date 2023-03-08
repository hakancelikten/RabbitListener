using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Application.IntegrationEvents.Events
{
    public class UrlListenerCreatedIntegrationEvent : IntegrationEvent
    {
        public string UrlAddress { get; set; }

    }
}
