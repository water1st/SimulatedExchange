using System.Collections.Generic;

namespace SimulatedExchange.Messages
{
    public interface IMessageHandlerFactory
    {
        IEnumerable<IMessageHandler<TMessage>> GetHandlers<TMessage>(TMessage @event) where TMessage : IMessage;
    }
}
