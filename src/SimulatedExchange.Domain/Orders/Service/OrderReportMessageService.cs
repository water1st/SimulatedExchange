using SimulatedExchange.Bus;
using SimulatedExchange.Events;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Orders
{
    public class OrderReportMessageService :
        IEventHandler<NewOrderEvent>,
        IEventHandler<CancelOrderEvent>,
        IEventHandler<PartialTransactionEvent>,
        IEventHandler<AllTransactionEvent>
    {
        private readonly IMessageBus messageBus;
        public OrderReportMessageService(IMessageBus messageBus)
        {
            this.messageBus = messageBus;
        }

        public async Task Handle(CancelOrderEvent @event)
        {
            var message = new OrderReportMessage();

            message.Id = @event.Id.ToString();
            message.Status = (int)OrderStatus.Canceled;

            await messageBus.SendAsync(message);
        }

        public async Task Handle(NewOrderEvent @event)
        {
            var message = new OrderReportMessage();

            message.Id = @event.Id.ToString();
            message.Status = (int)OrderStatus.Opened;

            await messageBus.SendAsync(message);
        }

        public async Task Handle(AllTransactionEvent @event)
        {
            var message = new OrderReportMessage();

            message.Id = @event.Id.ToString();
            message.Status = (int)OrderStatus.FullTransaction;

            await messageBus.SendAsync(message);
        }

        public async Task Handle(PartialTransactionEvent @event)
        {
            var message = new OrderReportMessage();

            message.Id = @event.Id.ToString();
            message.Status = (int)OrderStatus.PartialTransaction;

            await messageBus.SendAsync(message);
        }

    }
}
