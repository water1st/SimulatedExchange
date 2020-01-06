using SimulatedExchange.DataAccess;
using SimulatedExchange.DataAccess.ReportingTransaction;
using SimulatedExchange.Events;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Orders
{
    public class OrderReportingService :
        IEventHandler<NewOrderEvent>,
        IEventHandler<PartialCancelOrderEvent>,
        IEventHandler<FullCancelOrderEvent>,
        IEventHandler<PartialTransactionEvent>,
        IEventHandler<FullTransactionEvent>
    {
        private readonly ITransactionBus bus;

        public OrderReportingService(ITransactionBus bus)
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
            transaction.DateTime = @event.DateTime.DateTime;
            transaction.Status = (int)OrderStatus.Opened;
            transaction.Volume = 0;

            await bus.SendAsync(transaction);
        }

        public async Task Handle(PartialCancelOrderEvent @event)
        {
            var transaction = new UpdateOrderStatusTransaction();

            transaction.Id = @event.Id.ToString();
            transaction.Status = (int)OrderStatus.PartialCanceled;
            transaction.DateTime = @event.DateTime.DateTime;

            await bus.SendAsync(transaction);
        }

        public async Task Handle(FullTransactionEvent @event)
        {
            var transaction = new UpdateOrderTransaction();

            transaction.Status = (int)OrderStatus.FullTransaction;
            transaction.Volume = @event.Amount;
            transaction.Id = @event.Id.ToString();
            transaction.DateTime = @event.DateTime.DateTime;

            await bus.SendAsync(transaction);
        }

        public async Task Handle(FullCancelOrderEvent @event)
        {
            var transaction = new UpdateOrderStatusTransaction();

            transaction.Id = @event.Id.ToString();
            transaction.Status = (int)OrderStatus.FullCanceled;
            transaction.DateTime = @event.DateTime.DateTime;

            await bus.SendAsync(transaction);
        }

        public async Task Handle(PartialTransactionEvent @event)
        {
            var transaction = new UpdateOrderTransaction();

            transaction.Status = (int)OrderStatus.PartialTransaction;
            transaction.Volume = @event.Amount;
            transaction.Id = @event.Id.ToString();
            transaction.DateTime = @event.DateTime.DateTime;

            await bus.SendAsync(transaction);
        }
    }
}
