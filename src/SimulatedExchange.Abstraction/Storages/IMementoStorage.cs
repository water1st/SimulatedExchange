using SimulatedExchange.Domain;
using System;
using System.Threading.Tasks;

namespace SimulatedExchange.Storages
{
    public interface IMementoStorage
    {
        Task<BaseMemento> GetMemento(Guid aggregateId);
        Task SaveMemento(BaseMemento memento);
    }
}
