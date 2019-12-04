using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatedExchange.Events
{
    public interface IEvent
    {
        Guid Id { get; }
    }
}
