using MediatR;
using RabbitListener.Application.DTOs;
using RabbitListener.Application.Features.Queries.Url.CheckAllUrl;
using RabbitListener.Application.Features.Queries.Url.GetAllUrl;
using RabbitListener.Domain.Entities;

namespace RabbitListener
{
    public class UrlCheckOperation
    {
        IMediator mediator;

        public UrlCheckOperation(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<CheckAllUrlQueryResponse> checkAllUrl(List<GetAllUrlQueryResponse> list)
        {
            var urlCheckList = list.Select(p => new UrlCheckObject
            {
                Address = p.UrlAddress
            }).ToList();

            return await mediator.Send(new CheckAllUrlQueryRequest() { urlCheckList = urlCheckList });
        }

    }
}