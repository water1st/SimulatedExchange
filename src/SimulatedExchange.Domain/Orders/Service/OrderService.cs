using SimulatedExchange.Bus;
using SimulatedExchange.Events;
using System;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Orders.Service
{
    public class OrderService : IOrderService,
        IEventHandler<NewOrderEvent>,
        IEventHandler<CancelOrderEvent>,
        IEventHandler<OrderTransactionEvent>
    {
        private readonly IRepository<Order> repository;
        private readonly IEventBus eventBus;
        private readonly IWriteOnlyRepotingBus repotingBus;

        public OrderService(IRepository<Order> repository,
            IEventBus eventBus,
            IWriteOnlyRepotingBus repotingBus)
        {
            this.repository = repository;
            this.eventBus = eventBus;
            this.repotingBus = repotingBus;
        }

        public async Task CancelOrderAsync(Guid id)
        {
            var order = await repository.GetByIdAsync(id);
            order.Cancel();
            await repository.SaveAsync(order);
            await eventBus.PublishAsync(order.UncommittedEvent);
            order.MarkEventCommited();
        }

        public async Task PlaceOrderAsync(OrderInfo orderInfo)
        {
            var order = new Order();
            order.PlaceOrder(orderInfo);
            await repository.SaveAsync(order);
            await eventBus.PublishAsync(order.UncommittedEvent);
            order.MarkEventCommited();
        }

        public async Task TransactionAsync(Guid Id, TransactionInfo info)
        {
            var order = await repository.GetByIdAsync(Id);
            order.Deal(info);
            await repository.SaveAsync(order);
            await eventBus.PublishAsync(order.UncommittedEvent);
            order.MarkEventCommited();
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

        public async Task Handle(OrderTransactionEvent @event)
        {
            var transaction = new UpdateOrderTransaction();

            transaction.Status = @event.OrderStatus;
            transaction.Volume = @event.Amount;

            await repotingBus.Write(transaction);
        }
    }
}
