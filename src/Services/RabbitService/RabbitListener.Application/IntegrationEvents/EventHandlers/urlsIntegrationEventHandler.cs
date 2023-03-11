using EventBus.Base.Abstraction;
using RabbitListener.Application.DTOs;
using RabbitListener.Application.IntegrationEvents.Events;
using RabbitListener.Application.Interfaces.Services;
using Serilog;
using System.Text.Json;

namespace RabbitListener.Application.IntegrationEvents.EventHandlers
{
    public class urlsIntegrationEventHandler : IIntegrationEventHandler<urlsIntegrationEvent>
    {
        private readonly IUrlService _urlService;
        public urlsIntegrationEventHandler(IUrlService urlService)
        {
            _urlService = urlService;
        }

        public Task Handle(urlsIntegrationEvent @event)
        {
            var urlCheckObject = new UrlCheckObject()
            {
                Url = @event.Url
            };
            var res = _urlService.CheckUrl(urlCheckObject);
            Log.Information(JsonSerializer.Serialize(res.Result));

            return Task.CompletedTask;

        }
    }

}
