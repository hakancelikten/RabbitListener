using MediatR;
using RabbitListener.Application.Interfaces.Services;

namespace RabbitListener.Application.Features.Queries.Url.CheckAllUrl
{
    public class CheckAllUrlQueryHandler : IRequestHandler<CheckAllUrlQueryRequest, CheckAllUrlQueryResponse>
    {
        private readonly IUrlService _urlService;

        public CheckAllUrlQueryHandler(IUrlService urlService)
        {
            _urlService = urlService;
        }

        public async Task<CheckAllUrlQueryResponse> Handle(CheckAllUrlQueryRequest request, CancellationToken cancellationToken)
        {
            var res = await _urlService.CheckAllUrl(request.urlCheckList);

            return new CheckAllUrlQueryResponse() { urlCheckList = res };
        }
    }
}
