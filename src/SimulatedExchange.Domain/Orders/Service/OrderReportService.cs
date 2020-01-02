﻿using SimulatedExchange.ClientAdapter.Abstraction.Bus;
using SimulatedExchange.ClientAdapter.Messages;
using SimulatedExchange.Events;
using System;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Orders
{
    public class OrderReportService : IEventHandler<NewOrderEvent>,
        IEventHandler<CancelOrderEvent>,
        IEventHandler<TransactionEvent>
    {
        private readonly IMessageBus messageBus;
        private readonly IRepository<Order> repository;

        public OrderReportService(IMessageBus messageBus, IRepository<Order> repository)
        {
            this.messageBus = messageBus;
            this.repository = repository;
        }

        public async Task Handle(CancelOrderEvent @event)
        {
            var state = await GetState(@event.Id);
            await messageBus.SendAsync(new PartialCanceledMessage { State = state });
        }

        public async Task Handle(NewOrderEvent @event)
        {
            var state = await GetState(@event.Id);
            await messageBus.SendAsync(new NewOrderMessage { State = state });
        }

        public async Task Handle(TransactionEvent @event)
        {
            var state = await GetState(@event.Id);
            await messageBus.SendAsync(new FullTransactionMessage { State = state });
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
