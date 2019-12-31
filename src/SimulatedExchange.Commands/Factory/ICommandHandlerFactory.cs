using SimulatedExchange.Commands.Handlers;

namespace SimulatedExchange.Commands.Factory
{
    internal interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand> GetHandler<TCommand>() where TCommand : ICommand;
    }
}