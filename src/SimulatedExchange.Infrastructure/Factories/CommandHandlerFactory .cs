using SimulatedExchange.Commands;
using System;

namespace SimulatedExchange.Infrastructure.Factories
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider provider;

        public CommandHandlerFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public ICommandHandler<TCommand> GetHandler<TCommand>(TCommand command) where TCommand : Command
        {
            var type = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            return (ICommandHandler<TCommand>)provider.GetService(type);
        }
    }
}
