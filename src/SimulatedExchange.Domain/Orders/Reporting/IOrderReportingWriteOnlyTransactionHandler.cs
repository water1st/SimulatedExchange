using SimulatedExchange.Reporting;

namespace SimulatedExchange.Domain.Orders
{
    public interface IOrderReportingWriteOnlyTransactionHandler :
        IReportingWriteOnlyTransactionHandler<AddOrderTransaction>,
        IReportingWriteOnlyTransactionHandler<UpdateOrderStatusTransaction>,
        IReportingWriteOnlyTransactionHandler<UpdateOrderTransaction>
    {

    }
}
