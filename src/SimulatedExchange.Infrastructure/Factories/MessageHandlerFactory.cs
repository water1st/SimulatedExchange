using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimulatedExchange.Infrastructure.Factories
{
    public class MessageHandlerFactory : IMessageHandlerFactory
    {
        private readonly IServiceProvider provider;

        public MessageHandlerFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IEnumerable<IMessageHandler<TMessage>> GetHandlers<TMessage>(TMessage message) where TMessage : IMessage
        {
            var type = typeof(IMessageHandler<>).MakeGenericType(message.GetType());
            return provider.GetServices(type).Select(handler => (IMessageHandler<TMessage>)handler);
        }
    }
}
