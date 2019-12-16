using System;
using System.Collections.Generic;
using System.Text;
using SimulatedExchange.Applications.DTO;
using SimulatedExchange.Queries.Orders;

namespace SimulatedExchange.Applications.Mapper
{
    public class OrderServiceMapper : IOrderServiceMapper
    {
        public OrderDetial Map(IOrderDetial detial)
        {
            throw new NotImplementedException();
        }

        public OrderList Map(IOrderList list)
        {
            var result = new OrderList();
            foreach (var item in list)
            {
                result.Add(Map(item));
            }
            result.PagingInfo = list.Page;

            return result;
        }

        public OrderListItem Map(IOrderListItem listItem)
        {
            throw new NotImplementedException();
        }
    }
}
