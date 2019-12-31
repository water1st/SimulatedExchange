using SimulatedExchange.DataAccess.ReportingTransaction;
using SimulatedExchange.Queries.Orders;
using System;

namespace SimulatedExchange.Queries.Mapper
{
    internal class OrderMapper : IOrderMapper
    {
        public GetOrderQueryResult MapToGetOrderQueryResult(GetOrderTransactionResult item)
        {
            throw new NotImplementedException();
        }

        public GetOrdersQueryResult MapToGetOrdersQueryResult(GetOrdersTransactionResult items)
        {
            throw new NotImplementedException();
        }
    }
}
