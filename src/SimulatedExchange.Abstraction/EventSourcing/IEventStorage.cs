using SimulatedExchange.Domain;
using SimulatedExchange.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimulatedExchange.EventSourcing
{
    public interface IEventStorage
    {
        Task<IEnumerable<Event>> GetEventsAsync(Guid aggregateId);
        Task<IEnumerable<Event>> GetEventsAsync(Guid aggregateId, int maxVersion);
        Task SaveEventsAsync<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot;
    }
}
