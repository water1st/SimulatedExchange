using SimulatedExchange.Bus;
using SimulatedExchange.Events;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Orders
{
    public class OrderWriteReportingService :
        IEventHandler<NewOrderEvent>,
        IEventHandler<CancelOrderEvent>,
        IEventHandler<PartialTransactionEvent>,
        IEventHandler<AllTransactionEvent>
    {
        private readonly IWriteOnlyRepotingBus repotingBus;
        public OrderWriteReportingService(IWriteOnlyRepotingBus repotingBus)
        {
            this.repotingBus = repotingBus;
        }

        public async Task Handle(NewOrderEvent @event)
        {
            var transaction = new AddOrderTransaction();
            transaction.Amount = @event.Amount;
            transaction.Exchange = (int)@event.Exchange;
            transaction.Id = @event.Id.ToString();
            transaction.Price = @event.Price;
            transaction.Symbols = @event.Symbols.ToString();
            transaction.Type = (int)@event.Type;

            await repotingBus.Write(transaction);
        }

        public async Task Handle(CancelOrderEvent @event)
        {
            var transaction = new UpdateOrderStatusTransaction();

            transaction.Id = @event.Id.ToString();
            transaction.Status = OrderStatus.Canceled;

            await repotingBus.Write(transaction);
        }

        public async Task Handle(PartialTransactionEvent @event)
        {
            var transaction = new UpdateOrderTransaction();

            transaction.Status = OrderStatus.PartialTransaction;
            transaction.Volume = @event.Amount;

            await repotingBus.Write(transaction);
        }

        public async Task Handle(AllTransactionEvent @event)
        {
            var transaction = new UpdateOrderTransaction();

            transaction.Status = OrderStatus.FullTransaction;
            transaction.Volume = @event.Amount;

            await repotingBus.Write(transaction);
        }
    }
}
