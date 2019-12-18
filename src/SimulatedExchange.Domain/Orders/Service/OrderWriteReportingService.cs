﻿using SimulatedExchange.Bus;
using SimulatedExchange.Events;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Orders
{
    public class OrderWriteReportingService : IEventHandler<NewOrderEvent>,
        IEventHandler<CancelOrderEvent>,
        IEventHandler<TransactionEvent>
    {
        private readonly IWriteOnlyRepotingBus repotingBus;
        public OrderWriteReportingService(IWriteOnlyRepotingBus repotingBus)
        {
            this.repotingBus = repotingBus;
        }

        public async Task Handle(NewOrderEvent @event)
        {
            var transaction = new AddOrderTransaction();
            transaction.Amount = @event.Amount;
            transaction.Exchange = (int)@event.Exchange;
            transaction.Id = @event.AggregateId.ToString();
            transaction.Price = @event.Price;
            transaction.Symbols = @event.Symbols.ToString();
            transaction.Type = (int)@event.Type;

            await repotingBus.Write(transaction);
        }

        public async Task Handle(CancelOrderEvent @event)
        {
            var transaction = new UpdateOrderStatusTransaction();

            transaction.Id = @event.AggregateId.ToString();
            transaction.Status = OrderStatus.Canceled;

            await repotingBus.Write(transaction);
        }

        public async Task Handle(TransactionEvent @event)
        {
            var transaction = new UpdateOrderTransaction();

            transaction.Status = @event.OrderStatus;
            transaction.Volume = @event.Amount;
            transaction.Id = @event.AggregateId.ToString();

            await repotingBus.Write(transaction);
        }
    }
}
