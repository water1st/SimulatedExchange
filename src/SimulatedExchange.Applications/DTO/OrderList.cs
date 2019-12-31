using System.Collections.Generic;

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
