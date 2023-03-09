using MediatR;
using RabbitListener.Application.Features.Queries.Url.GetAllUrl;

namespace RabbitListener
{
    public class UrlRepoOperation
    {

        IMediator mediator;

        public UrlRepoOperation(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<List<GetAllUrlQueryResponse>> getAllUrl()
        {
            return await mediator.Send(new GetAllUrlQueryRequest());
        }
    }
}