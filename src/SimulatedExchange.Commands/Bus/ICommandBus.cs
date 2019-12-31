using System.Threading.Tasks;

namespace SimulatedExchange.Commands.Bus
{
    public interface ICommandBus
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
