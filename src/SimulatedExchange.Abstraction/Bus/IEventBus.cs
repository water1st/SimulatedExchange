using SimulatedExchange.Events;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimulatedExchange.Bus
{
    public interface IEventBus
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event;
        Task PublishAsync<TEvent>(IEnumerable<TEvent> @events) where TEvent : Event;
    }
}
