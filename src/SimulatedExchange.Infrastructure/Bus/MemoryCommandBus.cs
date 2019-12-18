using SimulatedExchange.Bus;
using SimulatedExchange.Commands;
using SimulatedExchange.Exceptions;
using System.Threading.Tasks;

namespace SimulatedExchange.Infrastructure.Bus
{
    public class MemoryCommandBus : ICommandBus
    {
        private readonly ICommandHandlerFactory factory;

        public MemoryCommandBus(ICommandHandlerFactory factory)
        {
            this.factory = factory;
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : Command
        {
            await SendCommandAsync(Converter.ChangeType(command, command.GetType()));
        }

        private async Task SendCommandAsync<TCommand>(TCommand command) where TCommand : Command
        {
            var handler = factory.GetHandler<TCommand>();
            if (handler != null)
            {
                await handler.ExecuteAsync(command).ConfigureAwait(false);
            }
            else
            {
                throw new UnregisteredDomainCommandException("找不到命令执行器");
            }
        }
    }
}
