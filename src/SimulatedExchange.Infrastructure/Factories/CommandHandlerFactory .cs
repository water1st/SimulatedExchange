using SimulatedExchange.Commands;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace SimulatedExchange.Infrastructure.Factories
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider provider;

        public CommandHandlerFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public ICommandHandler<TCommand> GetHandler<TCommand>() where TCommand : Command
        {
            return provider.GetService<ICommandHandler<TCommand>>();
        }
    }
}
