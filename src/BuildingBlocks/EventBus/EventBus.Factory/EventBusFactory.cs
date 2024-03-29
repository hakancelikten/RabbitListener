﻿using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.RabbitMQ;

namespace EventBus.Factory
{
    public class EventBusFactory
    {
        public static IEventBus Create(EventBusConfig config, IServiceProvider serviceProvider)
        {
            // Şuan için sadece RabbitMQ olduğu için default olarak rabbitmq dönmekteyim.
            return new EventBusRabbitMQ(config, serviceProvider);

        }
    }
}
