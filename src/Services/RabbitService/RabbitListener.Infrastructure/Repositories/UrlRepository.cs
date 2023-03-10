using Microsoft.EntityFrameworkCore;
using RabbitListener.Application.Interfaces.Repositories;
using RabbitListener.Domain.Entities;
using RabbitListener.Infrastructure.Context;
using System;
using System.Collections.Generic;
namespace RabbitListener.Infrastructure.Repositories
{
    public class UrlRepository : GenericRepository<Url>, IUrlRepository
    {
        /* Dummy data dönebilmek için ezilmiştir. */
        public override Task<List<Url>> GetAll()
        {
            var list = new List<Url>()
            {
                new Url() { Id = Guid.NewGuid(), CreateDate = DateTime.UtcNow, UrlAddress = "https://www.akakce.com/brosurlerde-en-iyi-fiyatlar" },
                new Url() { Id = Guid.NewGuid(), CreateDate = DateTime.UtcNow, UrlAddress = "https://www.akakce.com/televizyon.html" },
                new Url() { Id = Guid.NewGuid(), CreateDate = DateTime.UtcNow, UrlAddress = "https://www.akakce.com/cep-telefonu.html" },
                new Url() { Id = Guid.NewGuid(), CreateDate = DateTime.UtcNow, UrlAddress = "https://www.akakce.com" },
            };
            return Task.FromResult(list);
        }

        public UrlRepository() : base(null)
        {

        }
    }
}
