using SimulatedExchange.Domain.Orders.Service;
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
    }
}
