using SimulatedExchange.DataAccess;
using SimulatedExchange.DataAccess.ReportingTransaction;
using SimulatedExchange.Queries.Mapper;
using SimulatedExchange.Queries.Orders;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatedExchange.Queries.Handlers.Orders
{
    internal class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, GetOrderQueryResult>
    {
        private readonly ITransactionBus bus;
        private readonly IOrderMapper mapper;

        public GetOrderQueryHandler(ITransactionBus bus, IOrderMapper mapper)
        {
            this.bus = bus;
            this.mapper = mapper;
        }

        public async Task<GetOrderQueryResult> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var transaction = new GetOrderTransaction { Id = Guid.Parse(request.Id) };
            var transactionResult = await bus.SendAsync(transaction);
            var result = mapper.MapToGetOrderQueryResult(transactionResult);
            return result;
        }
    }
}
