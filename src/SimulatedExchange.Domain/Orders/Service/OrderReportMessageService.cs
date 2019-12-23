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
            await Handle(@event as OrderEvent);
        }

        public async Task Handle(NewOrderEvent @event)
        {
            await Handle(@event as OrderEvent);
        }

        public async Task Handle(TransactionEvent @event)
        {
            await Handle(@event as OrderEvent);
        }

        private async Task Handle(OrderEvent @event)
        {
            var message = new OrderReportMessage();

            message.Id = @event.AggregateId.ToString();
            message.Status = (int)@event.Status;
            message.Datetime = @event.DateTime.ToUnixTimeSeconds();

            await messageBus.SendAsync(message);
        }

    }
}
