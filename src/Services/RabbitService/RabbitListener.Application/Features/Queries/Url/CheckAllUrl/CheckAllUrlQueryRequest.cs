using MediatR;
using RabbitListener.Application.DTOs;

namespace RabbitListener.Application.Features.Queries.Url.CheckAllUrl
{
    public class CheckAllUrlQueryRequest : IRequest<CheckAllUrlQueryResponse>
    {
        public List<UrlCheckObject> UrlCheckList;
    }
}
