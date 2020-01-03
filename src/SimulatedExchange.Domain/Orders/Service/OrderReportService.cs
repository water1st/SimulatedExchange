using SimulatedExchange.ClientAdapter.Abstraction.Bus;
using SimulatedExchange.ClientAdapter.Messages;
using SimulatedExchange.ClientAdapter.Messages.Orders;
using SimulatedExchange.Events;
using System;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Orders
{
    public class OrderReportService :
        IEventHandler<NewOrderEvent>,
        IEventHandler<PartialCancelOrderEvent>,
        IEventHandler<FullCancelOrderEvent>,
        IEventHandler<PartialTransactionEvent>,
        IEventHandler<FullTransactionEvent>
    {
        private readonly IMessageBus messageBus;
        private readonly IRepository<Order> repository;

        public OrderReportService(IMessageBus messageBus, IRepository<Order> repository)
        {
            this.messageBus = messageBus;
            this.repository = repository;
        }

        public async Task Handle(PartialCancelOrderEvent @event)
        {
            var state = await GetState(@event.Id);
            await messageBus.SendAsync(new OrderReportingMessage
            {
                Event = ClientAdapter.Messages.Orders.Events.PartialCancel,
                State = state
            });
        }

        public async Task Handle(NewOrderEvent @event)
        {
            var state = await GetState(@event.Id);
            await messageBus.SendAsync(new OrderReportingMessage
            {
                Event = ClientAdapter.Messages.Orders.Events.New,
                State = state
            });
        }

        public async Task Handle(FullTransactionEvent @event)
        {
            var state = await GetState(@event.Id);
            await messageBus.SendAsync(new OrderReportingMessage
            {
                Event = ClientAdapter.Messages.Orders.Events.FullDeal,
                State = state
            });
        }

        public async Task Handle(FullCancelOrderEvent @event)
        {
            var state = await GetState(@event.Id);
            await messageBus.SendAsync(new OrderReportingMessage
            {
                Event = ClientAdapter.Messages.Orders.Events.FullCanceled,
                State = state
            });
        }

        public async Task Handle(PartialTransactionEvent @event)
        {
            var state = await GetState(@event.Id);
            await messageBus.SendAsync(new OrderReportingMessage
            {
                Event = ClientAdapter.Messages.Orders.Events.PartialDeal,
                State = state
            });
        }

        private async Task<OrderState> GetState(Guid id)
        {
            var order = await repository.GetByIdAsync(id);
            var result = new OrderState();

            result.Id = id.ToString();
            result.PairSymbols = order.PairSymbols.ToString();
            result.Price = order.Price;
            result.Status = (int)order.Status;
            result.TotalAmount = order.TotalAmount;
            result.Type = (int)order.Type;
            result.Volume = (int)order.Volume;
            result.ClientId = order.ClientId;

            return result;
        }


    }
}
