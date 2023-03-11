using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitListener.Application;
using RabbitListener.Application.IntegrationEvents.EventHandlers;
using RabbitListener.Application.IntegrationEvents.Events;
using RabbitListener.Infrastructure;
using RabbitMQ.Client;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace RabbitListener.UnitTest
{
    [TestClass]
    public class RabbitListenerTest
    {
        private IMediator _mediator;
        private IServiceProvider _sp;
        private IEventBus _eventBus;
        private ServiceCollection _services;

        [TestMethod]
        public void send_message_to_rabbitmq()
        {
            var urlOperation = new UrlRepoOperation(_mediator);
            var address = urlOperation.getAllUrl();

            foreach (var item in address.Result.ToList())
            {
                _eventBus.Publish(new urlsIntegrationEvent(item.UrlAddress));
            }
        }
        public RabbitListenerTest()
        {
            _services = new();

            ConfigureService(_services);

            ConfigureLogging(_services);

            #region ServiceProvider

            _sp = _services.BuildServiceProvider();

            _eventBus = _sp.GetRequiredService<IEventBus>();

            _mediator = _sp.GetRequiredService<IMediator>();

            #endregion
        }
        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }
        private static void ConfigureLogging(IServiceCollection _services)
        {
            var environment = Environment.GetEnvironmentVariable("ASNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()

            .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
            .AddJsonFile(
                $"appsettings.{environment}.json",
                optional: true)
            .Build();

            Log.Logger = new LoggerConfiguration()
                            .Enrich.FromLogContext()
                            .Enrich.WithMachineName()
                            .WriteTo.Debug()
                            .WriteTo.Console()
                            .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
                            .ReadFrom.Configuration(configuration)
                            .CreateLogger();

            _services.AddLogging(builder => builder.AddSerilog(Log.Logger));

            var serviceCollection = new ServiceCollection();

        }
        private static void ConfigureService(IServiceCollection _services)
        {
            _services
            .AddPersistenceRegistration()
            .AddApplicationRegistration();

            _services.AddTransient<urlsIntegrationEventHandler>();

            _services.AddSingleton<IEventBus>(_sp =>
            {
                EventBusConfig config = new()
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "IntegrationEvent",
                    DefaultTopicName = "RMQ_Listener",
                    SubscriberClientAppName = "",
                    EventBusType = EventBusType.RabbitMQ,
                    Connection = new ConnectionFactory()
                    {
                        HostName = "localhost",
                        Port = 5672,
                        UserName = "guest",
                        Password = "guest"
                    }
                };
                return EventBusFactory.Create(config, _sp);
            });
        }
    }
}