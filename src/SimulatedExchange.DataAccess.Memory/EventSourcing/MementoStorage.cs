using SimulatedExchange.Domain;
using SimulatedExchange.EventSourcing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedExchange.DataAccess.Memory.EventSourcing
{
    public class MementoStorage : IMementoStorage
    {
        private static readonly ConcurrentDictionary<Guid, ConcurrentStack<BaseMemento>> mementos;
        static MementoStorage()
        {
            mementos = new ConcurrentDictionary<Guid, ConcurrentStack<BaseMemento>>();
        }

        public Task<BaseMemento> GetMementoAsync(Guid aggregateId)
        {
            BaseMemento result = null;
            if (mementos.TryGetValue(aggregateId, out var stack))
            {
                if (stack.TryPeek(out var value))
                {
                    result = value;
                }
            }
            return Task.FromResult(result);
        }

        public Task<BaseMemento> GetMementoAsync(Guid aggregateId, int maxVersion)
        {
            BaseMemento result = null;
            if (mementos.TryGetValue(aggregateId, out var stack))
            {
                result = stack.FirstOrDefault(m => m.Version <= maxVersion);
            }

            return Task.FromResult(result);
        }

        public Task SaveMementoAsync(BaseMemento memento)
        {
            var stack = mementos.GetOrAdd(memento.Id, id => new ConcurrentStack<BaseMemento>());
            stack.Push(memento);
            return Task.CompletedTask;
        }
    }
}
