using SimulatedExchange.Domain;
using SimulatedExchange.Events;
using SimulatedExchange.EventSourcing;
using SimulatedExchange.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedExchange.DataAccess.Memory.EventSourcing
{
    public class EventStorage : IEventStorage
    {
        private static readonly ConcurrentDictionary<Guid, IEnumerable<Event>> events;
        static EventStorage()
        {
            events = new ConcurrentDictionary<Guid, IEnumerable<Event>>();
        }

        public Task<IEnumerable<Event>> GetEventsAsync(Guid aggregateId)
        {
            if (events.TryGetValue(aggregateId, out var result))
            {
                return Task.FromResult(result);
            }
            throw new AggregateNotFoundException($"找不到聚合根：\"{aggregateId}\"");
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(Guid aggregateId, int maxVersion)
        {
            var datas = await GetEventsAsync(aggregateId);
            var result = datas.Where(e => e.Version <= maxVersion);
            return result;
        }

        public Task SaveEventsAsync<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot
        {
            events.TryRemove(aggregateRoot.Id, out _);
            events.TryAdd(aggregateRoot.Id, aggregateRoot.UncommittedEvent);
            return Task.CompletedTask;
        }
    }
}
