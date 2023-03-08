using AutoMapper;
using RabbitListener.Application.Features.Queries.GetAllUrl;
using RabbitListener.Domain.Entities;
using RabbitListener.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
