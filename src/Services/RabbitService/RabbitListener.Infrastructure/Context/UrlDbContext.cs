using Microsoft.EntityFrameworkCore;
using RabbitListener.Application.Interfaces.Repositories.Common;

namespace RabbitListener.Infrastructure.Context
{
    public class UrlDbContext : DbContext, IUnitOfWork

    {
        public UrlDbContext() : base() { }
        public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options)
        { }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
