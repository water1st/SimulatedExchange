using SimulatedExchange.ClientAdapter.Messages;
using System.Threading.Tasks;

namespace SimulatedExchange.ClientAdapter.Abstraction.Handlers
{
    public interface IMessageHandler<TMessage>
        where TMessage : IMessage
    {
        Task Handle(TMessage message);
    }
}
