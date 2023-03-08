using AutoMapper;
using MediatR;
using RabbitListener.Application.Interfaces.Repositories;
using RabbitListener.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Application.Features.Queries.GetAllUrl
{

    public class GetAllUrlQueryHandler : IRequestHandler<GetAllUrlQueryRequest, GetAllUrlQueryResponse>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IMapper _mapper;

        public GetAllUrlQueryHandler(IUrlRepository urlRepository, IMapper mapper)
        {
            _urlRepository = urlRepository;
            _mapper = mapper;
        }

        public async Task<GetAllUrlQueryResponse> Handle(GetAllUrlQueryRequest request, CancellationToken cancellationToken)
        {
            List<Url> urls = await _urlRepository.GetAll();
            var result = urls.Select(p => p.UrlAddress).ToList();
            return new GetAllUrlQueryResponse()
            {
                UrlAddress = result
            };
        }
    }
}
