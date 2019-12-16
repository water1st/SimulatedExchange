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
            //获取聚合根
            var order = await repository.GetByIdAsync(id);
            //取消订单
            order.Cancel();
            //持久化事件
            await repository.SaveAsync(order);
            //推送未发送事件
            await eventBus.PublishAsync(order.UncommittedEvent);
            //标记事件已发送
            order.MarkEventCommited();
        }

        public async Task PlaceOrderAsync(OrderInfo orderInfo)
        {
            //创建聚合根
            var order = new Order();
            //下单
            order.PlaceOrder(orderInfo);
            //持久化事件
            await repository.SaveAsync(order);
            //推送事件
            await eventBus.PublishAsync(order.UncommittedEvent);
            //标记事件已发送
            order.MarkEventCommited();
        }

        public async Task TransactionAsync(Guid Id, TransactionInfo info)
        {
            //获取聚合根
            var order = await repository.GetByIdAsync(Id);
            //交易
            order.Deal(info);
            //持久化事件
            await repository.SaveAsync(order);
            //推送事件
            await eventBus.PublishAsync(order.UncommittedEvent);
            //标记事件已发送
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
