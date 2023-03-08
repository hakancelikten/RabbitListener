using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RabbitListener.Application;
using RabbitListener.Application.IntegrationEvents.EventHandlers;
using RabbitListener.Application.IntegrationEvents.Events;
using RabbitListener.Infrastructure;
using RabbitMQ.Client;

namespace RabbitListener
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new();

            ConfigureService(services);

            #region ServiceProvider
            var sp = services.BuildServiceProvider();

            var eventBus = sp.GetRequiredService<IEventBus>();

            eventBus.Subscribe<UrlListenerCreatedIntegrationEvent, UrlListenerCreatedIntegrationEventHandler>();

            var mediator = sp.GetRequiredService<IMediator>();
            #endregion

            var urlOperation = new UrlRepoOperation(mediator);

            var address = urlOperation.getAllUrl();

            var healthCheckOperation = new UrlCheckOperation(address.Result.UrlAddress.ToList());

            var logObject = healthCheckOperation.GetStatusCode();

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
}