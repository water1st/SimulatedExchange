using System.Threading.Tasks;

namespace SimulatedExchange.Queries
{
    internal interface IQueryHandler<TQuery, TQueryResult>
        where TQuery : IQuery<TQueryResult>
    {
        Task<TQueryResult> QueryAsync(TQuery query);
    }
}
