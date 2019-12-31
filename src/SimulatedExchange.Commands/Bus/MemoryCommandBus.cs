using SimulatedExchange.Commands.Factory;
using System.Threading.Tasks;

namespace SimulatedExchange.Commands.Bus
{
    internal class MemoryCommandBus : ICommandBus
    {
        private readonly ICommandHandlerFactory factory;

        public MemoryCommandBus(ICommandHandlerFactory factory)
        {
            this.factory = factory;
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = factory.GetHandler<TCommand>();
            await handler.ExecuteAsync(command);
        }
    }
}
