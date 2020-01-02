using System;

namespace SimulatedExchange.Domain
{
    public class BaseMemento
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
}
