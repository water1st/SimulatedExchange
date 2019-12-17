using SimulatedExchange.Applications.DTO;
using SimulatedExchange.Applications.Mapper;
using SimulatedExchange.Bus;
using SimulatedExchange.Commands.Commands;
using SimulatedExchange.Queries;
using SimulatedExchange.Queries.Orders;
using SimulatedExchange.Reporting;
using System;
using System.Threading.Tasks;

namespace SimulatedExchange.Applications.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IQueryBus queryBus;
        private readonly ICommandBus commandBus;
        private readonly IOrderServiceMapper mapper;

        public OrderServices(IQueryBus queryBus, ICommandBus commandBus, IOrderServiceMapper mapper)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
            this.mapper = mapper;
        }

        public async Task CalcelOrderAsync(string id)
        {
            var command = new CancelOrderCommand(Guid.Parse(id));
            await commandBus.SendAsync(command);
        }

        public async Task CreateNewOrder(OrderInfo orderInfo)
        {
            var command = new AddOrderCommand(Guid.NewGuid(),
                orderInfo.PairSymbols, orderInfo.Price,
                orderInfo.Amount, orderInfo.Exchange, orderInfo.Type);

            await commandBus.SendAsync(command);
        }

        public async Task<OrderDetial> GetIdAsync(string id)
        {
            var query = new GetOrderTransaction { Id = Guid.Parse(id) };

            var result = await queryBus.SendAsync<GetOrderTransaction, IOrderDetial>(query);

            return mapper.Map(result);
        }

        public async Task<OrderList> GetList(int pageIndex, int pageSize)
        {
            var query = new GetOrdersTransaction { Paging = new QueryPagingInfo { PageIndex = pageIndex, PageSize = pageSize } };

            var result = await queryBus.SendAsync<GetOrdersTransaction, IOrderList>(query);

            return mapper.Map(result);
        }

        public async Task TransactionAsync(string id, decimal amount, decimal price)
        {
            var command = new OrderTransactionCommand(Guid.Parse(id), price, amount);

            await commandBus.SendAsync(command);

        }
    }
}
