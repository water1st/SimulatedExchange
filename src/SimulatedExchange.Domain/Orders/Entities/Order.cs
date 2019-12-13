using SimulatedExchange.Events;
using System;

namespace SimulatedExchange.Domain.Orders
{
    public class Order : AggregateRoot,
        IAggregateRootEventHandler<NewOrderEvent>,
        IAggregateRootEventHandler<CancelOrderEvent>,
        IAggregateRootEventHandler<OrderTransactionEvent>
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

        public void PlaceOrder(OrderInfo orderInfo)
        {
            var @event = new NewOrderEvent(orderInfo.Symbols, orderInfo.Price, orderInfo.Amount, orderInfo.Exchange, OrderType.Limit);
            ApplyEvent(@event);
        }

        public void Cancel()
        {
            var @event = new CancelOrderEvent();
            ApplyEvent(@event);
        }

        public void Deal(TransactionInfo info)
        {
            var @event = new OrderTransactionEvent(info.Price, info.Amount);
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

        public void Handle(NewOrderEvent @event)
        {
            Exchange = @event.Exchange;
            TotalAmount = @event.Amount;
            Exchange = @event.Exchange;
            Price = @event.Price;
            PairSymbols = @event.Symbols;
            Type = @event.Type;
            Status = OrderStatus.Opened;
        }

        public void Handle(CancelOrderEvent @event)
        {
            Status = OrderStatus.Canceled;
        }

        public void Handle(OrderTransactionEvent @event)
        {
            var volume = @event.Amount + Volume;
            if (volume > TotalAmount)
            {
                throw new ArgumentOutOfRangeException("成交量大于委托量");
            }
            else if (volume == TotalAmount)
            {
                Status = OrderStatus.FullTransaction;
            }
            else
            {
                Status = OrderStatus.PartialTransaction;
            }
            @event.OrderStatus = Status;
            Price = @event.Price;
        }

    }
}
