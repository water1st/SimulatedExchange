using System.Collections.Generic;

namespace SimulatedExchange.Events
{
    public interface IEventHandlerFactory
    {
        IEnumerable<IEventHandler<TEvent>> GetHandlers<TEvent>() where TEvent : Event;
    }
}
