using SimulatedExchange.Applications.DTO;
using SimulatedExchange.Queries.Orders;

namespace SimulatedExchange.Applications.Mapper
{
    public interface IOrderServiceMapper
    {
        OrderDetial Map(GetOrderQueryResult detial);
        OrderList Map(GetOrdersQueryResult list);
        CurrentPagingInfo Map(Queries.PagingInfo pagingInfo);
    }
}
