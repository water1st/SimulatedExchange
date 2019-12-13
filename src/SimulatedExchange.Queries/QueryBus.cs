using SimulatedExchange.Bus;
using System.Threading.Tasks;

namespace SimulatedExchange.Queries
{
    public class QueryBus : IQueryBus
    {
        private readonly IReadOnlyReportingBus bus;

        public QueryBus(IReadOnlyReportingBus bus)
        {
            this.bus = bus;
        }

        public async Task<TQueryResult> Read<TQueryarameter, TQueryResult>(TQueryarameter parameter)
            where TQueryarameter : class
            where TQueryResult : class
        {
            return await bus.Read<TQueryarameter, TQueryResult>(parameter);
        }
    }
}
