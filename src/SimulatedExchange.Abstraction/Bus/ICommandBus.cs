using SimulatedExchange.Commands;
using System.Threading.Tasks;

namespace SimulatedExchange.Bus
{
    public interface ICommandBus
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : Command;
    }
}
