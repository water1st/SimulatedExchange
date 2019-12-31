using SimulatedExchange.ClientAdapter.Abstraction.Handlers;
using SimulatedExchange.ClientAdapter.Messages;
using System.Collections.Generic;

namespace SimulatedExchange.ClientAdapter.Factories
{
    internal interface IMessageHanderFactory
    {
        IEnumerable<IMessageHandler<TMessage>> GetHandlers<TMessage>()
            where TMessage : IMessage;
    }
}
