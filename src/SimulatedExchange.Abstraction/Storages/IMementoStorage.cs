﻿using SimulatedExchange.Domain;
using System;
using System.Threading.Tasks;

namespace SimulatedExchange.Storages
{
    public interface IMementoStorage
    {
        Task<BaseMemento> GetMementoAsync(Guid aggregateId);
        Task<BaseMemento> GetMementoAsync(Guid aggregateId, int maxVersion);
        Task SaveMementoAsync(BaseMemento memento);
    }
}
