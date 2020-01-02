using SimulatedExchange.DataAccess.ReportingTransaction;
using static SimulatedExchange.DataAccess.ReportingTransaction.Orders.OrderReporting;
using static SimulatedExchange.DataAccess.ReportingTransaction.GetOrdersTransactionResult;

namespace SimulatedExchange.DataAccess.Mapper
{
    public interface IOrderMapper
    {
        GetOrderTransactionResult MapToGetOrderTransactionResult(PersistentObject persistentObject);
        GetOrdersTransactionResultItem MapToGetOrdersTransactionResultItem(PersistentObject persistentObject);
        PersistentObject MapFromAddOrderTransaction(AddOrderTransaction transaction);
    }
}
