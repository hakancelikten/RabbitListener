using RabbitListener.Application.DTOs;
using RabbitListener.Application.Interfaces.Services;

namespace RabbitListener.Infrastructure.Services
{
    public class UrlService : IUrlService
    {
        private readonly HttpClient client = new HttpClient();
        public async Task<List<UrlCheckObject>> CheckAllUrl(List<UrlCheckObject> urlCheckList)
        {
            foreach (var item in urlCheckList)
            {
                var res = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, item.ToString()));
                item.StatusCode = res.StatusCode.ToString();
            }
            return urlCheckList;
        }
    }
}
