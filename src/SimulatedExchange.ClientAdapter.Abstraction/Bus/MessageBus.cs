using SimulatedExchange.ClientAdapter.Factories;
using SimulatedExchange.ClientAdapter.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimulatedExchange.ClientAdapter.Abstraction.Bus
{
    internal class MessageBus : IMessageBus
    {
        private readonly IMessageHanderFactory factory;

        public MessageBus(IMessageHanderFactory factory)
        {
            this.factory = factory;
        }

        public async Task SendAsync<TMessage>(TMessage message) where TMessage : IMessage
        {
            var handlers = factory.GetHandlers<TMessage>();
            foreach (var handler in handlers)
            {
                await handler.Handle(message).ConfigureAwait(false);
            }
        }

        public async Task SendAsync<TMessage>(IEnumerable<TMessage> messages) where TMessage : IMessage
        {
            var handlers = factory.GetHandlers<TMessage>();
            foreach (var message in messages)
            {
                foreach(var handler in handlers)
                {
                    await handler.Handle(message).ConfigureAwait(false);
                }
            }
        }
    }
}
