﻿using SimulatedExchange.Domain.Exceptions;
using SimulatedExchange.Events;
using System;

namespace SimulatedExchange.Domain.Orders
{
    public class Order : AggregateRoot,
        IAggregateRootEventHandler<NewOrderEvent>,
        IAggregateRootEventHandler<CancelOrderEvent>,
        IAggregateRootEventHandler<TransactionEvent>
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

        private void CheckOrder()
        {
            if (Status == OrderStatus.Canceled || Status == OrderStatus.Canceling)
            {
                throw new OrderIsCanceledException("订单已被取消");
            }
            if (Status == OrderStatus.FullTransaction)
            {
                throw new OrderIsFullTranscationException("订单已完全成交");
            }
        }

        public void PlaceOrder(OrderInfo orderInfo)
        {
            if (Status != OrderStatus.Opened)
            {
                throw new OrderIsExistExcetpion("订单已存在");
            }
            var @event = new NewOrderEvent
            {
                AggregateId = Id,
                Symbols = orderInfo.Symbols,
                Price = orderInfo.Price,
                Amount = orderInfo.Amount,
                Exchange = orderInfo.Exchange,
                Type = OrderType.Limit
            };
            ApplyEvent(@event);
        }

        public void Cancel()
        {
            CheckOrder();

            var @event = new CancelOrderEvent { AggregateId = Id };
            ApplyEvent(@event);
        }

        public void Deal(TransactionInfo info)
        {
            CheckOrder();

            var volume = Volume + info.Amount;
            var @event = new TransactionEvent { AggregateId = Id, Amount = volume, Price = info.Price };

            if (volume > TotalAmount)
            {
                throw new ArgumentOutOfRangeException("成交量大于委托总量");
            }
            else if (volume == TotalAmount)
            {
                @event.OrderStatus = OrderStatus.FullTransaction;
            }
            else
            {
                @event.OrderStatus = OrderStatus.PartialTransaction;
            }

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

        public void Handle(TransactionEvent @event)
        {
            Price = @event.Price;
            Volume = @event.Amount;
            Status = @event.OrderStatus;
        }
    }
}
