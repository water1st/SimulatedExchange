using System;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain
{
    public interface IRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot, new()
    {
        Task SaveAsync(TAggregateRoot aggregateRoot);
        Task<TAggregateRoot> GetByIdAsync(Guid primarykey, int maxVersion);
        Task<TAggregateRoot> GetByIdAsync(Guid primarykey);
    }
}
