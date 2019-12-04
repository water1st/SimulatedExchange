using Microsoft.Extensions.Logging;
using NeoSmart.AsyncLock;
using SimulatedExchange.Domain;
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
        private readonly ILogger logger;

        public Repository(IEventStorage eventStorage, IMementoStorage mementoStorage, ILogger<Repository<TAggregateRoot>> logger)
        {
            this.eventStorage = eventStorage;
            this.mementoStorage = mementoStorage;
            this.logger = logger;
        }
        public virtual async Task<TAggregateRoot> GetById(Guid id)
        {
            var memento = await mementoStorage.GetMemento(id);
            var events = await eventStorage.GetEvents(id);

            var aggregateRoot = new TAggregateRoot();
            if (memento != null)
            {
                events = events.Where(@event => @event.Version >= memento.Version);
                aggregateRoot.SetMemento(memento);
            }

            aggregateRoot.LoadsFromHistory(events);

            return aggregateRoot;
        }

        public virtual async Task Save(TAggregateRoot aggregateRoot)
        {
            if (aggregateRoot.UncommittedEvent.Any())
            {
                using (await @lock.LockAsync())
                {
                    if (aggregateRoot.Version != -1)
                    {
                        var item = await GetById(aggregateRoot.Id);
                        if (item.Version != aggregateRoot.Version)
                        {
                            var ex = new Exception("聚合根已被其他线程修改");
                            logger.LogWarning(ex, ex.Message);
                            throw ex;
                        }
                    }

                    await eventStorage.SaveEvents(aggregateRoot);
                }
            }
        }
    }
}
