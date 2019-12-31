using SimulatedExchange.DataAccess;
using SimulatedExchange.DataAccess.ReportingTransaction;
using SimulatedExchange.Queries.Mapper;
using SimulatedExchange.Queries.Orders;
using System.Threading.Tasks;

namespace SimulatedExchange.Queries.Handlers.Orders
{
    internal class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, GetOrdersQueryResult>
    {
        private readonly ITransactionBus bus;
        private readonly IOrderMapper mapper;

        public GetOrdersQueryHandler(ITransactionBus bus, IOrderMapper mapper)
        {
            this.bus = bus;
            this.mapper = mapper;
        }

        public async Task<GetOrdersQueryResult> QueryAsync(GetOrdersQuery query)
        {
            var transaction = new GetOrdersTransaction();
            if (transaction.PagingOptions != null
                && transaction.PagingOptions.PageIndex > 0
                && transaction.PagingOptions.PageSize > 0)
            {
                transaction.PagingOptions = new DataAccess.ReportingTransaction.PagingOptions
                {
                    PageIndex = transaction.PagingOptions.PageIndex,
                    PageSize = transaction.PagingOptions.PageSize
                };
            }

            var transactionResult = await bus.SendAsync(transaction);
            var result = mapper.MapToGetOrdersQueryResult(transactionResult);
            return result;
        }
    }
}
