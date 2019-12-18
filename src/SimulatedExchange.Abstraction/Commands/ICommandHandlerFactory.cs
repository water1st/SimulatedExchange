namespace SimulatedExchange.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand> GetHandler<TCommand>() where TCommand : Command;
    }
}
