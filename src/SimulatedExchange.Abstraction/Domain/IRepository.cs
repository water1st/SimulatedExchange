using System;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain
{
    public interface IRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot, new()
    {
        Task Save(TAggregateRoot aggregateRoot);
        Task<TAggregateRoot> GetById(Guid primarykey);
    }
}
