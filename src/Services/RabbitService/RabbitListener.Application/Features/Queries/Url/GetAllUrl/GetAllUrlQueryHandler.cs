using AutoMapper;
using MediatR;
using RabbitListener.Application.Interfaces.Repositories;

namespace RabbitListener.Application.Features.Queries.Url.GetAllUrl
{
    public class GetAllUrlQueryHandler : IRequestHandler<GetAllUrlQueryRequest, List<GetAllUrlQueryResponse>>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IMapper _mapper;

        public GetAllUrlQueryHandler(IUrlRepository urlRepository, IMapper mapper)
        {
            _urlRepository = urlRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllUrlQueryResponse>> Handle(GetAllUrlQueryRequest request, CancellationToken cancellationToken)
        {
            var urls = await _urlRepository.GetAll();

            return urls.Select(p => new GetAllUrlQueryResponse
            {
                UrlAddress = p.UrlAddress
            }).ToList();
        }
    }
}
