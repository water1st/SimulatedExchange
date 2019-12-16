using SimulatedExchange.Applications.DTO;
using SimulatedExchange.Queries.Orders;

namespace SimulatedExchange.Applications.Mapper
{
    public interface IOrderServiceMapper
    {
        OrderDetial Map(IOrderDetial detial);
        OrderList Map(IOrderList list);
        OrderListItem Map(IOrderListItem listItem);
    }
}
