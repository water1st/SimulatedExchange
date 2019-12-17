using SimulatedExchange.Bus;
using System;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Orders.Service
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> repository;
        private readonly IEventBus eventBus;

        public OrderService(IRepository<Order> repository,
            IEventBus eventBus)
        {
            this.repository = repository;
            this.eventBus = eventBus;
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


    }
}
