using SimulatedExchange.Bus;
using SimulatedExchange.Events;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Orders
{
    public class OrderReportMessageService : IEventHandler<NewOrderEvent>,
        IEventHandler<CancelOrderEvent>,
        IEventHandler<TransactionEvent>
    {
        private readonly IMessageBus messageBus;
        public OrderReportMessageService(IMessageBus messageBus)
        {
            this.messageBus = messageBus;
        }

        public async Task Handle(CancelOrderEvent @event)
        {
            var message = new OrderReportMessage();

            message.Id = @event.AggregateId.ToString();
            message.Status = (int)OrderStatus.Canceled;

            await messageBus.SendAsync(message);
        }

        public async Task Handle(NewOrderEvent @event)
        {
            var message = new OrderReportMessage();

            message.Id = @event.AggregateId.ToString();
            message.Status = (int)OrderStatus.Opened;

            await messageBus.SendAsync(message);
        }

        public async Task Handle(TransactionEvent @event)
        {
            var message = new OrderReportMessage();

            message.Id = @event.AggregateId.ToString();
            message.Status = (int)@event.OrderStatus;

            await messageBus.SendAsync(message);
        }

    }
}
