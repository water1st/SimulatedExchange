using SimulatedExchange.Bus;
using System;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Orders.Service
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> repository;
        private readonly IEventBus bus;

        public OrderService(IRepository<Order> repository, IEventBus bus)
        {
            this.repository = repository;
            this.bus = bus;
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
            await bus.PublishAsync(order.UncommittedEvent);
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
            await bus.PublishAsync(order.UncommittedEvent);
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
            await bus.PublishAsync(order.UncommittedEvent);
            //标记事件已发送
            order.MarkEventCommited();
        }
    }
}
