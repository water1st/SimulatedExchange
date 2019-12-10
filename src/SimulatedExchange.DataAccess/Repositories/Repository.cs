using Microsoft.Extensions.Logging;
using NeoSmart.AsyncLock;
using SimulatedExchange.Domain;
using SimulatedExchange.Exceptions;
using SimulatedExchange.Storages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimulatedExchange.DataAccess.Repositories
{
    public class Repository<TAggregateRoot> : IRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot, new()
    {
        private readonly AsyncLock @lock = new AsyncLock();
        private readonly IEventStorage eventStorage;
        private readonly IMementoStorage mementoStorage;

        public Repository(IEventStorage eventStorage, IMementoStorage mementoStorage)
        {
            this.eventStorage = eventStorage;
            this.mementoStorage = mementoStorage;
        }
        public virtual async Task<TAggregateRoot> GetByIdAsync(Guid id)
        {
            var memento = await mementoStorage.GetMementoAsync(id);
            var events = await eventStorage.GetEventsAsync(id);

            var aggregateRoot = new TAggregateRoot();
            if (memento != null)
            {
                events = events.Where(@event => @event.Version >= memento.Version);
                aggregateRoot.SetMemento(memento);
            }

            aggregateRoot.RestoreEvents(events);

            return aggregateRoot;
        }

        public virtual async Task SaveAsync(TAggregateRoot aggregateRoot)
        {
            if (aggregateRoot.UncommittedEvent.Any())
            {
                using (await @lock.LockAsync())
                {
                    if (aggregateRoot.Version != -1)
                    {
                        var item = await GetByIdAsync(aggregateRoot.Id);
                        if (item.Version != aggregateRoot.Version)
                        {
                            throw new ConcurrencyException($"聚合根 \"{aggregateRoot.Id}\" 已被修改");
                        }
                    }

                    await eventStorage.SaveEventsAsync(aggregateRoot);
                }
            }
        }
    }
}
