using System.Threading.Tasks;

namespace SimulatedExchange.Queries
{
    public interface IQueryBus
    {
        Task<TQueryResult> SendAsync<TQueryarameter, TQueryResult>(TQueryarameter parameter)
            where TQueryResult : class
            where TQueryarameter : class;
    }
}
