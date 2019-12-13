using System.Threading.Tasks;

namespace SimulatedExchange.Reporting
{
    public interface IReportingReadOnlyTransactionHandler<TReadParameter, TReadResult>
        where TReadParameter : class
        where TReadResult : class
    {
        Task<TReadResult> Read(TReadParameter readParameter);
    }
}
