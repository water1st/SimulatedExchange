using System;

namespace SimulatedExchange.Domain
{
    public class BaseMemento
    {
        public Guid AggregateRootId { get; set; }
        public int Version { get; set; }
    }
}
