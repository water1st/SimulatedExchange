using SimulatedExchange.Events;
using System.Collections.Generic;

namespace SimulatedExchange.Domain
{
    public interface IEventProvider
    {
        void RestoreEvents(IEnumerable<Event> history);
        IEnumerable<Event> UncommittedEvent { get; }
    }
}
