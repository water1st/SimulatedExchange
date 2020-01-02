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
        private static readonly ConcurrentDictionary<Guid, ConcurrentQueue<Event>> events;
        private readonly IMementoStorage mementoStorage;

        static EventStorage()
        {
            events = new ConcurrentDictionary<Guid, ConcurrentQueue<Event>>();
        }

        public EventStorage(IMementoStorage mementoStorage)
        {
            this.mementoStorage = mementoStorage;
        }

        public Task<IEnumerable<Event>> GetEventsAsync(Guid aggregateId)
        {
            if (events.TryGetValue(aggregateId, out var data))
            {
                IEnumerable<Event> result = data;
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

        public async Task SaveEventsAsync<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot
        {
            var queue = events.GetOrAdd(aggregateRoot.Id, id => new ConcurrentQueue<Event>());
            var version = aggregateRoot.Version;

            foreach (var @event in aggregateRoot.UncommittedEvent)
            {
                version++;
                @event.Version = version;
                if (version % 1024 == 0)
                {
                    var memento = aggregateRoot.GetMemento();
                    memento.Version = version;
                    await mementoStorage.SaveMementoAsync(memento);
                }

                queue.Enqueue(@event);
            }


        }
    }
}
