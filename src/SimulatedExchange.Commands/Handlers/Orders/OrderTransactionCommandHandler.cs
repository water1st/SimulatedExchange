using MediatR;
using SimulatedExchange.Domain.Orders;
using SimulatedExchange.Domain.Orders.Service;
using System.Threading;
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

        public async Task<Unit> Handle(OrderTransactionCommand request, CancellationToken cancellationToken)
        {
            await orderService.TransactionAsync(request.Id, new TransactionInfo
            {
                Amount = request.Amount,
                Price = request.Price
            }).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
