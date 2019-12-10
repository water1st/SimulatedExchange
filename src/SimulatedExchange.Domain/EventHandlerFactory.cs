using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Events;
using System;
using System.Collections.Generic;

namespace SimulatedExchange.Domain
{
    public class EventHandlerFactory : IEventHandlerFactory
    {
        private readonly IServiceProvider provider;

        public EventHandlerFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IEnumerable<IEventHandler<TEvent>> GetHandlers<TEvent>() where TEvent : Event
        {
            return provider.GetServices<IEventHandler<TEvent>>();
        }
    }
}
