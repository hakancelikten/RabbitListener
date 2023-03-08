using EventBus.Base.Abstraction;
using RabbitListener.Application.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
