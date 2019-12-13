using SimulatedExchange.Queries.Orders;
using SimulatedExchange.Reporting;
using System.Collections.Generic;

namespace SimulatedExchange.DataAccess.ReportingStorages.Orders
{
    public partial class OrderReportingStorage
    {
        private class OrderList : List<IOrderListItem>, IOrderList
        {
            public OrderList()
            {
            }

            public OrderList(IEnumerable<IOrderListItem> collection) : base(collection)
            {
            }

            public OrderList(int capacity) : base(capacity)
            {
            }

            public CurrentPagingInfo Page { get; set; }
        }
    }

}
