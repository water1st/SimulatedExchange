using System.Threading.Tasks;

namespace SimulatedExchange.Reporting
{
    public interface IReportingWriteOnlyTransactionHandler<TWriteParameter>
        where TWriteParameter : class
    {
        Task Write(TWriteParameter parameter);
    }
}
