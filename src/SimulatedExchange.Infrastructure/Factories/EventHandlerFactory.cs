using SimulatedExchange.Events;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace SimulatedExchange.Infrastructure.Factories
{
    public class EventHandlerFactory : IEventHandlerFactory
    {
        private readonly IServiceProvider provider;

        public EventHandlerFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IEnumerable<IEventHandler<TEvent>> GetHandlers<TEvent>(TEvent @event) where TEvent : Event
        {
            var type = typeof(IEventHandler<>).MakeGenericType(@event.GetType());
            return provider.GetServices(type).Select(handler => (IEventHandler<TEvent>)handler);
        }
    }
}
