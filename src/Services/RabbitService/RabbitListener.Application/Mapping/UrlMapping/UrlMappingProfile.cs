using AutoMapper;
using RabbitListener.Application.Features.Queries.Url.GetAllUrl;
using RabbitListener.Domain.Entities;

namespace RabbitListener.Application.Mapping.UrlMapping
{
    public class UrlMappingProfile : Profile
    {
        public UrlMappingProfile()
        {
            CreateMap<Url, GetAllUrlQueryResponse>()
                .ReverseMap();
        }
    }
}
