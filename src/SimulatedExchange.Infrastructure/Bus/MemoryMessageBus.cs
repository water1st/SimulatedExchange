using SimulatedExchange.Bus;
using SimulatedExchange.Messages;
using System.Threading.Tasks;

namespace SimulatedExchange.Infrastructure.Bus
{
    public class MemoryMessageBus : IMessageBus
    {
        private readonly IMessageHandlerFactory factory;

        public MemoryMessageBus(IMessageHandlerFactory factory)
        {
            this.factory = factory;
        }

        public async Task SendAsync<TMessage>(TMessage message) where TMessage : IMessage
        {
            var handlers = factory.GetHandlers(message);
            foreach (var handler in handlers)
            {
                await handler.Handle(message).ConfigureAwait(false);
            }
        }
    }
}
