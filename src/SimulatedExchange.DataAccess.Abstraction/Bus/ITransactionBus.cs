using SimulatedExchange.DataAccess.ReportingTransaction;
using System.Threading.Tasks;

namespace SimulatedExchange.DataAccess
{
    public interface ITransactionBus
    {
        Task SendAsync<TReportingTransaction>(TReportingTransaction transaction)
            where TReportingTransaction : IReportingTransaction;

        Task<TResult> SendAsync<TResult>(IReportingTransaction<TResult> transaction);
    }
}
