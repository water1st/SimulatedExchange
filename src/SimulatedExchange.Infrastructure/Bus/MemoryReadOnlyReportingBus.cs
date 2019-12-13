using SimulatedExchange.Bus;
using SimulatedExchange.Exceptions;
using SimulatedExchange.Reporting;
using System.Threading.Tasks;

namespace SimulatedExchange.Infrastructure.Bus
{
    public class MemoryReadOnlyReportingBus : IReadOnlyReportingBus
    {
        private readonly IReportingReadOnlyTransactionHandlerFactory factory;

        public MemoryReadOnlyReportingBus(IReportingReadOnlyTransactionHandlerFactory factory)
        {
            this.factory = factory;
        }

        public async Task<TQueryResult> Read<TQueryarameter, TQueryResult>(TQueryarameter parameter)
            where TQueryarameter : class
            where TQueryResult : class
        {
            var reader = factory.GetReader<TQueryarameter, TQueryResult>();
            if (reader != null)
            {
                return await reader.Read(parameter);
            }
            else
            {
                throw new UnregisteredReaderException("找不到只读库读取器");
            }
        }
    }
}
