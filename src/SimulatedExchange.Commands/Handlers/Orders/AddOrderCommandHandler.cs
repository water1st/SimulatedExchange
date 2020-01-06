using SimulatedExchange.Domain.Orders;
using SimulatedExchange.Domain.Orders.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatedExchange.Commands.Handlers
{
    internal class AddOrderCommandHandler : ICommandHandler<AddOrderCommand, Guid>
    {
        private readonly IOrderService orderService;

        public AddOrderCommandHandler(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<Guid> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var id = await orderService.PlaceOrderAsync(new OrderInfo
            {
                Amount = request.Amount,
                Exchange = (Exchange)request.Exchange,
                Price = request.Price,
                Symbols = request.Symbols
            });

            return id;
        }
    }
}
