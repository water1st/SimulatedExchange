using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.ClientAdapter.Abstraction.Handlers;
using SimulatedExchange.ClientAdapter.Messages;
using System;
using System.Collections.Generic;

namespace SimulatedExchange.ClientAdapter.Factories
{
    internal class MessageHandlerFactory : IMessageHanderFactory
    {
        private readonly IServiceProvider provider;

        public MessageHandlerFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IEnumerable<IMessageHandler<TMessage>> GetHandlers<TMessage>() where TMessage : IMessage
        {
            var handlers = provider.GetServices<IMessageHandler<TMessage>>();
            return handlers;
        }
    }
}
