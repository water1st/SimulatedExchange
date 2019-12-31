using SimulatedExchange.DataAccess;
using SimulatedExchange.DataAccess.ReportingTransaction;
using SimulatedExchange.Events;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Orders
{
    public class OrderReportingService : IEventHandler<NewOrderEvent>,
        IEventHandler<CancelOrderEvent>,
        IEventHandler<TransactionEvent>
    {
        private readonly ITransactionBus bus;

        public OrderReportingService(ITransactionBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(NewOrderEvent @event)
        {
            var transaction = new AddOrderTransaction();
            transaction.ClientId = @event.ClientId;
            transaction.Amount = @event.Amount;
            transaction.Exchange = (int)@event.Exchange;
            transaction.Id = @event.AggregateId.ToString();
            transaction.Price = @event.Price;
            transaction.Symbols = @event.Symbols.ToString();
            transaction.Type = (int)@event.Type;
            transaction.DateTime = @event.DateTime.DateTime;

            await bus.SendAsync(transaction);
        }

        public async Task Handle(CancelOrderEvent @event)
        {
            var transaction = new UpdateOrderStatusTransaction();

            transaction.Id = @event.AggregateId.ToString();
            transaction.Status = (int)@event.Status;
            transaction.DateTime = @event.DateTime.DateTime;

            await bus.SendAsync(transaction);
        }

        public async Task Handle(TransactionEvent @event)
        {
            var transaction = new UpdateOrderTransaction();

            transaction.Status = (int)@event.Status;
            transaction.Volume = @event.Amount;
            transaction.Id = @event.AggregateId.ToString();
            transaction.DateTime = @event.DateTime.DateTime;

            await bus.SendAsync(transaction);
        }
    }
}
