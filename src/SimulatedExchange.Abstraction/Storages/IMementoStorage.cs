using SimulatedExchange.Domain;
using System;
using System.Threading.Tasks;

namespace SimulatedExchange.Storages
{
    public interface IMementoStorage
    {
        Task<BaseMemento> GetMementoAsync(Guid aggregateId);
        Task SaveMementoAsync(BaseMemento memento);
    }
}
