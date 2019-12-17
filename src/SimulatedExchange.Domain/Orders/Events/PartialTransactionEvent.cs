﻿using SimulatedExchange.Events;
using System;

namespace SimulatedExchange.Domain.Orders
{
    public class PartialTransactionEvent : Event
    {
        public PartialTransactionEvent(Guid id, decimal price, decimal amount)
        {
            Price = price;
            Amount = amount;
            AggregateId = id;
        }

        public decimal Price { get; }
        public decimal Amount { get; }
    }
}
