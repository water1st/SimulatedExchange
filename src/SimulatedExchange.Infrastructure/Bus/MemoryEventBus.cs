using SimulatedExchange.Bus;
using SimulatedExchange.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimulatedExchange.Infrastructure.Bus
{
    public class MemoryEventBus : IEventBus
    {
        private readonly IEventHandlerFactory handlerFactory;

        public MemoryEventBus(IEventHandlerFactory handlerFactory)
        {
            this.handlerFactory = handlerFactory;
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event
        {
            var handlers = handlerFactory.GetHandlers(@event);
            foreach (var handler in handlers)
            {
                await handler.Handle(@event).ConfigureAwait(false);
            }
        }

        public async Task PublishAsync<TEvent>(IEnumerable<TEvent> events) where TEvent : Event
        {
            foreach (var @event in events)
            {
                await PublishAsync(@event).ConfigureAwait(false);
            }
        }
    }
}
