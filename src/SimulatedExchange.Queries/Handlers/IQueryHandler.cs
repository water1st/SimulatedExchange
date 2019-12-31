using MediatR;
using System.Threading.Tasks;

namespace SimulatedExchange.Queries
{
    internal interface IQueryHandler<TQuery, TQueryResult> : IRequestHandler<TQuery, TQueryResult>
        where TQuery : IQuery<TQueryResult>
    {

    }
}
