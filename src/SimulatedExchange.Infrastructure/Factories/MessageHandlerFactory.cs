using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Messages;
using System;
using System.Collections.Generic;

namespace SimulatedExchange.Infrastructure.Factories
{
    public class MessageHandlerFactory : IMessageHandlerFactory
    {
        private readonly IServiceProvider provider;

        public MessageHandlerFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IEnumerable<IMessageHandler<TMessage>> GetHandlers<TMessage>() where TMessage : IMessage
        {
            return provider.GetServices<IMessageHandler<TMessage>>();
        }
    }
}
