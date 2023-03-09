using MediatR;
using Microsoft.Extensions.Logging;
using RabbitListener.Application.DTOs;
using RabbitListener.Application.Features.Queries.Url.CheckAllUrl;
using RabbitListener.Application.Features.Queries.Url.GetAllUrl;
using Serilog.Core;
using System.Text.Json;

namespace RabbitListener
{
    public class UrlCheckOperation
    {
        IMediator mediator;
        ILogger _logger;

        public UrlCheckOperation(IMediator mediator, ILogger logger)
        {
            this.mediator = mediator;
            _logger = logger;
        }

        public async Task<CheckAllUrlQueryResponse> checkAllUrl(List<GetAllUrlQueryResponse> list)
        {
            _logger.LogInformation("head requests is starting");

            var urlCheckList = list.Select(p => new UrlCheckObject
            {
                Address = p.UrlAddress,
            }).ToList();
            return await mediator.Send(new CheckAllUrlQueryRequest() { UrlCheckList = urlCheckList });
        }

    }
}