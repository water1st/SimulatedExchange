using System.Threading.Tasks;

namespace SimulatedExchange.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        Task ExecuteAsync(TCommand command);
    }
}
