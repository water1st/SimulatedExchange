using System.Threading.Tasks;

namespace SimulatedExchange.Commands.Handlers
{
    internal interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task ExecuteAsync(TCommand command);
    }
}
