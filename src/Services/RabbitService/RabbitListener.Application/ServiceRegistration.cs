using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RabbitListener.Application.Mapping.UrlMapping;
using System;
using System.Reflection;

namespace RabbitListener.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);
            services.AddAutoMapper(typeof(UrlMappingProfile));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

            return services;
        }
    }
}
