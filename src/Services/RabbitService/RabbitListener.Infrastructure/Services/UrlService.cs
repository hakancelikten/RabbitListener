using RabbitListener.Application.DTOs;
using RabbitListener.Application.Interfaces.Services;
using System.Net;

namespace RabbitListener.Infrastructure.Services
{
    public class UrlService : IUrlService
    {
        private readonly HttpClient client = new HttpClient();
        public async Task<List<UrlCheckObject>> CheckAllUrl(List<UrlCheckObject> urlCheckList)
        {
            foreach (var item in urlCheckList)
            {
                var res = client.Send(new HttpRequestMessage(HttpMethod.Head, item.Address.ToString()));
                item.StatusCode = res.StatusCode.ToString();
            }
            return urlCheckList;
        }
    }
}
