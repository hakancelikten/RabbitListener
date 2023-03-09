using RabbitListener.Application.Interfaces.Repositories.Common;
using RabbitListener.Domain.Entities;

namespace RabbitListener.Application.Interfaces.Repositories
{
    public interface IUrlRepository : IGenericRepository<Url>
    {
    }
}
