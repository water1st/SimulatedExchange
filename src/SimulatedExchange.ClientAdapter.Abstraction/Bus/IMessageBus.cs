using SimulatedExchange.ClientAdapter.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimulatedExchange.ClientAdapter.Abstraction.Bus
{
    public interface IMessageBus
    {
        Task SendAsync<TMessage>(TMessage message)
            where TMessage : IMessage;

        Task SendAsync<TMessage>(IEnumerable<TMessage> messages)
            where TMessage : IMessage;
    }
}
