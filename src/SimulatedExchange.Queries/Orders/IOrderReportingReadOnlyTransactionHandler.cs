using SimulatedExchange.Reporting;

namespace SimulatedExchange.Queries.Orders
{
    public interface IOrderReportingReadOnlyTransactionHandler : 
        IReportingReadOnlyTransactionHandler<GetOrderTransaction, IOrderDetial>,
        IReportingReadOnlyTransactionHandler<GetOrdersTransaction, IOrderList>
    {

    }
}
