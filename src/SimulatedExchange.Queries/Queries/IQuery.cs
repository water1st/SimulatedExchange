using MediatR;

namespace SimulatedExchange.Queries
{
    public interface IQuery<TQueryResult> : IRequest<TQueryResult> { }
}
