using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitListener.Application;
using RabbitListener.Application.DTOs;
using RabbitListener.Application.IntegrationEvents.EventHandlers;
using RabbitListener.Application.IntegrationEvents.Events;
using RabbitListener.Infrastructure;
using RabbitMQ.Client;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;
using System.Text.Json;

namespace RabbitListener
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new();

            ConfigureService(services);

            ConfigureLogging(services);

            #region ServiceProvider

            var sp = services.BuildServiceProvider();

            var eventBus = sp.GetRequiredService<IEventBus>();

            var logger = sp.GetRequiredService<ILogger<Program>>();

            eventBus.Subscribe<UrlListenerCreatedIntegrationEvent, UrlListenerCreatedIntegrationEventHandler>();

            var mediator = sp.GetRequiredService<IMediator>();

            #endregion

            #region Operations

            var urlOperation = new UrlRepoOperation(mediator);

            var address = urlOperation.getAllUrl();

            var urlCheckOperation = new UrlCheckOperation(mediator, logger);

            var logObjects = urlCheckOperation.checkAllUrl(address.Result);

            logger.LogInformation($"checked all url: {JsonSerializer.Serialize(logObjects.Result.UrlCheckList)}");

            #endregion

            Console.WriteLine("This message write on ElasticSearch with serilog sink. Kibana serving on 5601 port.");

            Console.ReadLine();

        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }
        private static void ConfigureLogging(IServiceCollection services)
        {
            var environment = Environment.GetEnvironmentVariable("ASNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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

            services.AddLogging(builder => builder.AddSerilog(Log.Logger));
            //Log.Logger.Information("Seri Log is working");

            var serviceCollection = new ServiceCollection();

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