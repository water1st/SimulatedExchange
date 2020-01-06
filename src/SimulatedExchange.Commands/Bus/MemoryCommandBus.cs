using MediatR;
using System.Threading.Tasks;

namespace SimulatedExchange.Commands.Bus
{
    internal class MemoryCommandBus : ICommandBus
    {
        private readonly IMediator mediator;

        public MemoryCommandBus(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            await mediator.Send(command);
        }

        public async Task<TResult> SendAsync<TResult>(ICommand<TResult> command)
        {
            var result = await mediator.Send(command);
            return result;
        }
    }
}
