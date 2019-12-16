using SimulatedExchange.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatedExchange.Applications.DTO
{
    public class OrderList : List<OrderListItem>
    {
        public OrderList()
        {
        }

        public OrderList(IEnumerable<OrderListItem> collection) : base(collection)
        {
        }

        public OrderList(int capacity) : base(capacity)
        {
        }

        public CurrentPagingInfo PagingInfo { get; set; }
    }
}
