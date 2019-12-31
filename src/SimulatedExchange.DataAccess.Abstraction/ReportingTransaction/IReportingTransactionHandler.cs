using System.Threading.Tasks;

namespace SimulatedExchange.DataAccess.ReportingTransaction
{
    public interface IReportingTransactionHandler<in TTransaction>
        where TTransaction : IReportingTransaction
    {
        Task Handle(TTransaction transaction);
    }

    public interface IReportingTransactionHandler<in TTransaction, TResult>
        where TTransaction : IReportingTransaction<TResult>
    {
        Task<TResult> Handle(TTransaction transaction);
    }

}
