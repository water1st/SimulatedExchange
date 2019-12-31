using SimulatedExchange.DataAccess.ReportingTransaction;
using static SimulatedExchange.DataAccess.ReportingTransaction.Orders.OrderReporting;
using static SimulatedExchange.DataAccess.ReportingTransaction.GetOrdersTransactionResult;

namespace SimulatedExchange.DataAccess.Mapper
{
    internal interface IOrderMapper
    {
        GetOrderTransactionResult MapToGetOrderTransactionResult(PersistentObject persistentObject);
        GetOrdersTransactionResultItem MapToGetOrdersTransactionResultItem(PersistentObject persistentObject);
    }
}
