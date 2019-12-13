using SimulatedExchange.Bus;
using SimulatedExchange.Exceptions;
using SimulatedExchange.Reporting;
using System.Threading.Tasks;

namespace SimulatedExchange.Infrastructure.Bus
{
    public class MemoryWriteOnlyRepotingBus : IWriteOnlyRepotingBus
    {
        private readonly IReportingWriteOnlyTransactionHandlerFactory factory;

        public MemoryWriteOnlyRepotingBus(IReportingWriteOnlyTransactionHandlerFactory factory)
        {
            this.factory = factory;
        }

        public async Task Write<TWriterParameter>(TWriterParameter parameter) where TWriterParameter : class
        {
            var writer = factory.GetWriter<TWriterParameter>();
            if (writer != null)
            {
                await writer.Write(parameter).ConfigureAwait(false);
            }
            else
            {
                throw new UnregisteredWriterException("找不到写入器");
            }
        }
    }
}
