using SimulatedExchange.Domain.Orders;
using SimulatedExchange.Domain.Orders.Service;
using System.Threading.Tasks;

namespace SimulatedExchange.Commands.Handlers
{
    internal class AddOrderCommandHandler : ICommandHandler<AddOrderCommand>
    {
        private readonly IOrderService orderService;

        public AddOrderCommandHandler(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task ExecuteAsync(AddOrderCommand command)
        {
            await orderService.PlaceOrderAsync(new OrderInfo
            {
                Amount = command.Amount,
                Exchange = (Exchange)command.Exchange,
                Price = command.Price,
                Symbols = command.Symbols,
                ClientId = command.ClientId

            }).ConfigureAwait(false);
        }
    }
}
