namespace SimulatedExchange.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand> GetHandler<TCommand>(TCommand command) where TCommand : Command;
    }
}
