using MediatR;

namespace SimulatedExchange.DataAccess.ReportingTransaction
{
    public interface IReportingTransactionHandler<in TTransaction> : IRequestHandler<TTransaction>
        where TTransaction : IReportingTransaction
    {
    }

    public interface IReportingTransactionHandler<in TTransaction, TResult> : IRequestHandler<TTransaction, TResult>
        where TTransaction : IReportingTransaction<TResult>
    {
    }

}
