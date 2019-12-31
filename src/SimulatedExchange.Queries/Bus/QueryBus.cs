using MediatR;
using System.Threading.Tasks;

namespace SimulatedExchange.Queries.Bus
{
    internal class QueryBus : IQueryBus
    {
        private readonly IMediator mediator;

        public QueryBus(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<TQueryResult> SendAsync<TQueryResult>(IQuery<TQueryResult> query)
        {
            var result = await mediator.Send(query);
            return result;
        }
    }
}
