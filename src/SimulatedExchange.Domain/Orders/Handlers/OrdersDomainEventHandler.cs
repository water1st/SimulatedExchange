using SimulatedExchange.Bus;
using SimulatedExchange.Events;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Orders
{
    public class OrdersDomainEventHandler :
        IEventHandler<NewOrderEvent>,
        IEventHandler<CancelOrderEvent>,
        IEventHandler<OrderTransactionEvent>
    {
        private readonly IWriteOnlyRepotingBus bus;

        public OrdersDomainEventHandler(IWriteOnlyRepotingBus bus)
        {
            this.bus = bus;
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

            await bus.Write(transaction);
        }

        public async Task Handle(CancelOrderEvent @event)
        {
            var transaction = new UpdateOrderStatusTransaction();

            transaction.Id = @event.Id.ToString();
            transaction.Status = OrderStatus.Canceled;

            await bus.Write(transaction);
        }

        public async Task Handle(OrderTransactionEvent @event)
        {
            var transaction = new UpdateOrderTransaction();

            transaction.Status = @event.OrderStatus;
            transaction.Volume = @event.Amount;

            await bus.Write(transaction);
        }
    }
}
