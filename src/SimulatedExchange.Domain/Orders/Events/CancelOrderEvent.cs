using SimulatedExchange.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatedExchange.Domain.Orders
{
    public class CancelOrderEvent : Event
    {
        public CancelOrderEvent(Guid id)
        {
            AggregateId = id;
        }
    }
}
