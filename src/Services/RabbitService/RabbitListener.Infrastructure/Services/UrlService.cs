using RabbitListener.Application.DTOs;
using RabbitListener.Application.Interfaces.Services;
using System.Net;

namespace RabbitListener.Infrastructure.Services
{
    public class UrlService : IUrlService
    {
        private readonly HttpClient _client = new HttpClient();
        public Task<UrlCheckObject> CheckUrl(UrlCheckObject urlCheckObject)
        {
            var res = _client.Send(new HttpRequestMessage(HttpMethod.Head, urlCheckObject.Url));
            urlCheckObject.StatusCode = res.StatusCode.ToString();

            return Task.FromResult(urlCheckObject);
        }

    }
}
