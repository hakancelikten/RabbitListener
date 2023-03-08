using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Application.Features.Queries.GetAllUrl
{
    public class GetAllUrlQueryRequest : IRequest<GetAllUrlQueryResponse>
    {
    }
}
