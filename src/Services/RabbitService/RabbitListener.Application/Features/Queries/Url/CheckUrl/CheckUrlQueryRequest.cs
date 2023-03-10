using MediatR;
using RabbitListener.Application.DTOs;

namespace RabbitListener.Application.Features.Queries.Url.CheckUrl
{
    public class CheckUrlQueryRequest : IRequest<CheckUrlQueryResponse>
    {
        public UrlCheckObject UrlCheckObject;
    }
}
