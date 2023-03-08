using RabbitListener.Domain.Entities;
using RabbitListener.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Application.Interfaces.Repositories
{
    public interface IUrlRepository : IGenericRepository<Url>
    {
    }
}
