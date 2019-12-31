using SimulatedExchange.DataAccess.ReportingTransaction;
using SimulatedExchange.Queries.Orders;

namespace SimulatedExchange.Queries.Mapper
{
    public interface IOrderMapper
    {
        GetOrderQueryResult MapToGetOrderQueryResult(GetOrderTransactionResult item);
        GetOrdersQueryResult MapToGetOrdersQueryResult(GetOrdersTransactionResult items);
    }
}
