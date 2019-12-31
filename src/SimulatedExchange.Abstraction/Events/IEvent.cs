using System;

namespace SimulatedExchange.Events
{
    public interface IEvent
    {
        Guid Id { get; }
    }
}
