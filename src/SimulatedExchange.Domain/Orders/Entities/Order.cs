using SimulatedExchange.Domain.Orders.Events;
using SimulatedExchange.Events;
using System;

namespace SimulatedExchange.Domain.Orders.Entities
{
    public class Order : AggregateRoot,
        IAggregateRootEventHandler<NewOrder>,
        IAggregateRootEventHandler<CancelOrder>,
        IAggregateRootEventHandler<OrderTransaction>
    {
        //币对
        public PairSymbols PairSymbols { get; private set; }
        //委托价格
        public decimal Price { get; private set; }
        //成交量
        public decimal Volume { get; private set; }
        //委托总量
        public decimal TotalAmount { get; private set; }
        //交易所
        public Exchange Exchange { get; private set; }
        //订单类型
        public OrderType Type { get; private set; }
        //订单状态
        public OrderStatus Status { get; private set; }

        public void PlaceOrder()
        {
            var @event = new NewOrder();
            ApplyEvent(@event);
        }

        public void Cancel()
        {
            var @event = new CancelOrder();
            ApplyEvent(@event);
        }

        public void Deal(decimal price, decimal amount)
        {
            var @event = new OrderTransaction
            {
                Price = price,
                Amount = amount
            };

            ApplyEvent(@event);
        }

        public override BaseMemento GetMemento()
        {
            return new OrderMemento
            {
                TotalAmount = TotalAmount,
                Volume = Volume,
                Exchange = Exchange,
                AggregateRootId = Id,
                PairSymbols = PairSymbols,
                Price = Price,
                Status = Status,
                Type = Type,
                Version = Version
            };
        }

        public override void SetMemento(BaseMemento memento)
        {
            var orderMemento = (OrderMemento)memento;
            Id = orderMemento.AggregateRootId;
            TotalAmount = orderMemento.TotalAmount;
            Volume = orderMemento.Volume;
            Exchange = orderMemento.Exchange;
            PairSymbols = orderMemento.PairSymbols;
            Price = orderMemento.Price;
            Status = orderMemento.Status;
            Type = orderMemento.Type;
            Version = orderMemento.Version;
        }

        public void Handle(NewOrder @event)
        {
            throw new NotImplementedException();
        }

        public void Handle(CancelOrder @event)
        {
            throw new NotImplementedException();
        }

        public void Handle(OrderTransaction @event)
        {

        }

    }
}
