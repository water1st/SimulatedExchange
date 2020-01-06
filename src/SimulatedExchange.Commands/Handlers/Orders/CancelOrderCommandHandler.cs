using MediatR;
using SimulatedExchange.Domain.Orders.Service;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatedExchange.Commands.Handlers
{
    internal class CancelOrderCommandHandler : ICommandHandler<CancelOrderCommand>
    {
        private readonly IOrderService orderService;

        public CancelOrderCommandHandler(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task ExecuteAsync(CancelOrderCommand command)
        {
            await orderService.CancelOrderAsync(command.Id).ConfigureAwait(false);
        }

        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            await orderService.CancelOrderAsync(request.Id).ConfigureAwait(false);
            return Unit.Value;
        }
    }
}
