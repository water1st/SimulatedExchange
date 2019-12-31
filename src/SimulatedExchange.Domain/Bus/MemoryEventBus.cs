using SimulatedExchange.Bus;
using SimulatedExchange.Events;
using SimulatedExchange.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Bus
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
            await PublishEventAsync(Converter.ChangeType(@event, @event.GetType()));
        }

        public async Task PublishAsync<TEvent>(IEnumerable<TEvent> events) where TEvent : Event
        {
            foreach (var @event in events)
            {
                await PublishAsync(@event).ConfigureAwait(false);
            }
        }

        private async Task PublishEventAsync<TEvent>(TEvent @event) where TEvent : Event
        {
            var handlers = handlerFactory.GetHandlers<TEvent>();
            foreach (var handler in handlers)
            {
                await handler.Handle(@event).ConfigureAwait(false);
            }
        }
    }
}
