using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RabbitListener.Application;
using RabbitListener.Application.Features.Queries.GetAllUrl;
using RabbitListener.Application.IntegrationEvents.EventHandlers;
using RabbitListener.Application.IntegrationEvents.Events;
using RabbitListener.Domain.Entities;
using RabbitListener.Infrastructure;
using RabbitMQ.Client;
using System;
using System.Net.Http;


namespace RabbitListener
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new();

            ConfigureService(services);

            var sp = services.BuildServiceProvider();

            var eventBus = sp.GetRequiredService<IEventBus>();

            eventBus.Subscribe<UrlListenerCreatedIntegrationEvent, UrlListenerCreatedIntegrationEventHandler>();

            var mediator = sp.GetRequiredService<IMediator>();

            var urlOperation = new UrlOperation(mediator);

            var address = urlOperation.getAllUrl();

            var healthCheckOperation = new HealthCheckOperation(address.Result.UrlAddress.ToList());

            var logObject = healthCheckOperation.GetStatusCodes();

            Console.WriteLine("Application is running....");
            Console.ReadLine();

        }
        static bool IsRunningInContainer => bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inDocker) && inDocker;

        private static void ConfigureService(IServiceCollection services)
        {
            services
            .AddPersistenceRegistration()
            .AddApplicationRegistration();

            services.AddTransient<UrlListenerCreatedIntegrationEventHandler>();
            var host = IsRunningInContainer ? "host.docker.internal" : "localhost";
            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new()
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "IntegrationEvent",
                    DefaultTopicName = "RMQ_Listener",
                    SubscriberClientAppName = "EventBus.App",
                    EventBusType = EventBusType.RabbitMQ,
                    Connection = new ConnectionFactory()
                    {
                        HostName = host,
                        Port = 5672,
                        UserName = "guest",
                        Password = "guest"
                    }
                };

                return EventBusFactory.Create(config, sp);

            });
        }

    }
    public class HealthCheckOperation
    {
        private static readonly HttpClient client = new HttpClient();

        private List<string> address;

        private List<LogObject> logObject = new List<LogObject>();

        public HealthCheckOperation(List<string> address)
        {
            this.address = address;
        }

        public List<LogObject> GetStatusCodes()
        {
            foreach (var item in address)
            {
                var res = client.Send(new HttpRequestMessage(HttpMethod.Head, item.ToString()));
                logObject.Add(new LogObject() { Address = item.ToString(), StatusCode = res.StatusCode.ToString() });
            }
            return logObject;
        }

    }
    public class LogObject
    {
        public string Address { get; set; }
        public string StatusCode { get; set; }
    }
    public class UrlOperation
    {

        IMediator mediator;

        public UrlOperation(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<GetAllUrlQueryResponse> getAllUrl()
        {
            return await mediator.Send(new GetAllUrlQueryRequest());
        }
    }
}