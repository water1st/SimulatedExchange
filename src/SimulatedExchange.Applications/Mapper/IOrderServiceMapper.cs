using SimulatedExchange.Applications.DTO;
using SimulatedExchange.Queries.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatedExchange.Applications.Mapper
{
    public interface IOrderServiceMapper
    {
        OrderDetial Map(IOrderDetial detial);
        OrderList Map(IOrderList list);
        OrderListItem Map(IOrderListItem listItem);
    }
}
