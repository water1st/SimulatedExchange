using System.Threading.Tasks;

namespace SimulatedExchange.Bus
{
    public interface IReadOnlyReportingBus
    {
        Task<TQueryResult> Read<TQueryarameter, TQueryResult>(TQueryarameter parameter)
            where TQueryResult : class
            where TQueryarameter : class;
    }
}
