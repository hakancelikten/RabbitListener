using MediatR;
using Microsoft.EntityFrameworkCore;
using RabbitListener.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
