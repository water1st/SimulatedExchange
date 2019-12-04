using SimulatedExchange.Events;
using System.Collections.Generic;

namespace SimulatedExchange.Domain
{
    public interface IEventProvider
    {
        void LoadsFromHistory(IEnumerable<Event> history);
        IEnumerable<Event> UncommittedEvent { get; }
    }
}
