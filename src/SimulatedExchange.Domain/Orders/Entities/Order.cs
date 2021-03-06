﻿using SimulatedExchange.Domain.Exceptions;
using SimulatedExchange.Events;
using SimulatedExchange.Exceptions;
using System;

namespace SimulatedExchange.Domain.Orders
{
    public class Order : AggregateRoot,
        IAggregateRootEventHandler<NewOrderEvent>,
        IAggregateRootEventHandler<PartialCancelOrderEvent>,
        IAggregateRootEventHandler<FullCancelOrderEvent>,
        IAggregateRootEventHandler<PartialTransactionEvent>,
        IAggregateRootEventHandler<FullTransactionEvent>

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
            if (Status != OrderStatus.Opened)
            {
                throw new OrderIsExistExcetpion("订单已存在");
            }
            var @event = new NewOrderEvent
            {
                Id = Guid.NewGuid(),
                Symbols = orderInfo.Symbols,
                Price = orderInfo.Price,
                Amount = orderInfo.Amount,
                Exchange = orderInfo.Exchange,
                Type = OrderType.Limit,
                DateTime = DateTimeOffset.UtcNow
            };
            ApplyEvent(@event);
        }

        public void Cancel()
        {
            if (Status == OrderStatus.FullCanceled
                || Status == OrderStatus.PartialCanceled
                || Status == OrderStatus.Canceling)
            {
                throw new OrderIsCanceledException("订单已被取消");
            }
            if (Status == OrderStatus.FullTransaction)
            {
                throw new OrderHasBeenDealException("订单已完全成交");
            }

            Event @event = null;
            if (Status == OrderStatus.PartialTransaction)
            {
                @event = new PartialCancelOrderEvent { Id = Id, DateTime = DateTime.UtcNow };
            }
            else
            {
                @event = new FullCancelOrderEvent { Id = Id, DateTime = DateTime.UtcNow };
            }

            ApplyEvent(@event);
        }

        public void Deal(TransactionInfo info)
        {
            if (Status == OrderStatus.FullCanceled
                || Status == OrderStatus.PartialCanceled
                || Status == OrderStatus.Canceling)
            {
                throw new OrderIsCanceledException("订单已被取消");
            }
            if (Status == OrderStatus.FullTransaction)
            {
                throw new OrderHasBeenDealException("订单已完全成交");
            }
            if (info.Amount <= 0)
            {
                throw new InvalidValueException("数量：Amount不大于0");
            }
            if (info.Price <= 0)
            {
                throw new InvalidValueException("价格：Price不大于0");
            }

            var volume = Volume + info.Amount;
            Event @event = null;

            if (volume > TotalAmount)
            {
                throw new InvalidValueException("成交量大于委托总量");
            }

            else if (volume == TotalAmount)
            {
                @event = new FullTransactionEvent
                {
                    Id = Id,
                    Amount = volume,
                    Price = info.Price,
                    DateTime = DateTimeOffset.UtcNow
                };
            }
            else
            {
                @event = new PartialTransactionEvent
                {
                    Id = Id,
                    Amount = volume,
                    Price = info.Price,
                    DateTime = DateTimeOffset.UtcNow
                };
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
                Id = Id,
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
            Id = orderMemento.Id;
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
            Id = @event.Id;
        }

        public void Handle(PartialCancelOrderEvent @event)
        {
            Status = OrderStatus.PartialCanceled;
        }

        public void Handle(FullTransactionEvent @event)
        {
            Price = @event.Price;
            Volume = @event.Amount;
            Status = OrderStatus.FullTransaction;
        }

        public void Handle(FullCancelOrderEvent @event)
        {
            Status = OrderStatus.FullCanceled;
        }

        public void Handle(PartialTransactionEvent @event)
        {
            Price = @event.Price;
            Volume = @event.Amount;
            Status = OrderStatus.PartialTransaction;
        }
    }
}
