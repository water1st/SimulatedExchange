using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Commands.Handlers;
using System;

namespace SimulatedExchange.Commands.Factory
{
    internal class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider provider;

        public CommandHandlerFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public ICommandHandler<TCommand> GetHandler<TCommand>()
            where TCommand : ICommand
        {
            return provider.GetService<ICommandHandler<TCommand>>();
        }
    }
}
