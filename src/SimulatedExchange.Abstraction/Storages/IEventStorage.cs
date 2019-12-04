using SimulatedExchange.Domain;
using SimulatedExchange.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimulatedExchange.Storages
{
    public interface IEventStorage
    {
        Task<IEnumerable<Event>> GetEvents(Guid aggregateId);
        Task SaveEvents<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot;
    }
}
