using MediatR;
using SimulatedExchange.DataAccess.ReportingTransaction;
using System.Threading.Tasks;

namespace SimulatedExchange.DataAccess
{
    internal class TransactionBus : ITransactionBus
    {
        private readonly IMediator mediator;

        public TransactionBus(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task SendAsync<TReportingTransaction>(TReportingTransaction transaction) where TReportingTransaction : IReportingTransaction
        {
            await mediator.Send(transaction);
        }

        public async Task<TResult> SendAsync<TResult>(IReportingTransaction<TResult> transaction)
        {
            var result = await mediator.Send(transaction);
            return result;
        }
    }
}
