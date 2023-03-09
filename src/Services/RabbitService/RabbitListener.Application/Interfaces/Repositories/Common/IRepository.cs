
namespace RabbitListener.Application.Interfaces.Repositories.Common
{
    public interface IRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
