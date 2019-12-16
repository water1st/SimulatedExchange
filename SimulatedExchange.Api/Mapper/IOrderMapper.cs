using SimulatedExchange.Api.DTO;
using SimulatedExchange.Applications.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulatedExchange.Api.Mapper
{
    public interface IOrderMapper
    {
        GerOrderListResponse Map(OrderList orderLists);

        
    }
}
