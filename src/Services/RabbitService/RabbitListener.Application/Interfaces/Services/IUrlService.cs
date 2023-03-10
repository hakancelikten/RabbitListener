using RabbitListener.Application.DTOs;

namespace RabbitListener.Application.Interfaces.Services
{
    public interface IUrlService
    {
        Task<UrlCheckObject> CheckUrl(UrlCheckObject urlCheckObject);
    }
}
