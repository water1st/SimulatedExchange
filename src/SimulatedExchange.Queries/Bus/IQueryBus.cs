using System.Threading.Tasks;

namespace SimulatedExchange.Queries.Bus
{
    public interface IQueryBus
    {
        Task<TQueryResult> SendAsync<TQueryResult>(IQuery<TQueryResult> query);
    }
}
