using MediatR;
using RabbitListener.Application.Interfaces.Services;

namespace RabbitListener.Application.Features.Queries.Url.CheckUrl
{
    public class CheckUrlQueryHandler : IRequestHandler<CheckUrlQueryRequest, CheckUrlQueryResponse>
    {
        private readonly IUrlService _urlService;

        public CheckUrlQueryHandler(IUrlService urlService)
        {
            _urlService = urlService;
        }

        public async Task<CheckUrlQueryResponse> Handle(CheckUrlQueryRequest request, CancellationToken cancellationToken)
        {
            var res = await _urlService.CheckUrl(request.UrlCheckObject);

            return new CheckUrlQueryResponse() { UrlCheckList = res };
        }
    }
}
