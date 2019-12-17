using SimulatedExchange.Messages;
using System.Threading.Tasks;

namespace SimulatedExchange.Bus
{
    public interface IMessageBus
    {
        Task SendAsync<TMessage>(TMessage message)
            where TMessage : IMessage;
    }
}
