using RabbitListener.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Domain.Entities
{
    public class Url : BaseEntity
    {
        public String UrlAddress { get; set; }

    }
}
