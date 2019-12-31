using SimulatedExchange.Domain.Orders;
using SimulatedExchange.Domain.Orders.Service;
using System.Threading.Tasks;

namespace SimulatedExchange.Commands.Handlers
{
    internal class OrderTransactionCommandHandler : ICommandHandler<OrderTransactionCommand>
    {
        private readonly IOrderService orderService;

        public OrderTransactionCommandHandler(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task ExecuteAsync(OrderTransactionCommand command)
        {
            await orderService.TransactionAsync(command.Id, new TransactionInfo
            {
                Amount = command.Amount,
                Price = command.Price
            }).ConfigureAwait(false);
        }
    }
}
