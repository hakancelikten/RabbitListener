using MediatR;
using RabbitListener.Application.Features.Queries.Url.GetAllUrl;

namespace RabbitListener.UnitTest
{
    public class UrlRepoOperation
    {
        private IMediator _mediator;
        public UrlRepoOperation(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<GetAllUrlQueryResponse>> getAllUrl()
        {
            return await _mediator.Send(new GetAllUrlQueryRequest());
        }
    }
}