using Microsoft.Extensions.DependencyInjection;
using RabbitListener.Application.Interfaces.Repositories;
using RabbitListener.Application.Interfaces.Services;
using RabbitListener.Infrastructure.Repositories;
using RabbitListener.Infrastructure.Services;

namespace RabbitListener.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceRegistration(this IServiceCollection services)
        {
            /*
             * Database registration işlemi kapatılmıştır. 
             */

            //var DbConnectionString = "Data Source=localhost; Initial Catalog=RabbitListener;Persist Security Info=True;Integrated Security=True;";

            //services.AddDbContext<UrlDbContext>(opt =>
            //{
            //    opt.UseSqlServer(DbConnectionString);
            //    opt.EnableSensitiveDataLogging();
            //});

            //var optionsBuilder = new DbContextOptionsBuilder<UrlDbContext>().UseSqlServer(DbConnectionString);

            //using var dbContext = new UrlDbContext(optionsBuilder.Options);
            //dbContext.Database.EnsureCreated();
            //dbContext.Database.Migrate();

            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddScoped<IUrlService, UrlService>();

            return services;
        }
    }
}
