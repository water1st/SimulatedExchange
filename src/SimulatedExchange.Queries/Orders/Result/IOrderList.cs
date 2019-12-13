using SimulatedExchange.Reporting;
using System.Collections.Generic;

namespace SimulatedExchange.Queries.Orders
{
    public interface IOrderList : IReadOnlyList<IOrderListItem>
    {
        CurrentPagingInfo Page { get; }
    }
}
