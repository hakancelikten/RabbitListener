using RabbitListener.Application.DTOs;

namespace RabbitListener.Application.Interfaces.Services
{
    public interface IUrlService
    {
        Task<List<UrlCheckObject>> CheckAllUrl(List<UrlCheckObject> urlCheckList);
    }
}
