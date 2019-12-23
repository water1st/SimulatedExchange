using System;

namespace SimulatedExchange.Events
{
    public abstract class Event : IEvent
    {
        public int Version { get; set; }
        public Guid AggregateId { get; set; }
        public Guid Id { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
