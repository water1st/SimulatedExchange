using SimulatedExchange.Applications.DTO;
using SimulatedExchange.Applications.Mapper;
using SimulatedExchange.Applications.Validators;
using SimulatedExchange.Commands;
using SimulatedExchange.Commands.Bus;
using SimulatedExchange.Queries;
using SimulatedExchange.Queries.Bus;
using SimulatedExchange.Queries.Orders;
using System;
using System.Threading.Tasks;

namespace SimulatedExchange.Applications.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IQueryBus queryBus;
        private readonly ICommandBus commandBus;
        private readonly IOrderServiceMapper mapper;
        private readonly IOrderValidator validator;

        public OrderServices(IQueryBus queryBus, ICommandBus commandBus,
            IOrderServiceMapper mapper, IOrderValidator validator)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async Task CalcelOrderAsync(string id)
        {
            validator.VerifyId(id);

            var command = new CancelOrderCommand(Guid.Parse(id));
            await commandBus.SendAsync(command);
        }

        public async Task<string> CreateNewOrder(OrderInfo orderInfo)
        {
            validator.VerifyOrderInfo(orderInfo);

            var command = new AddOrderCommand(
                orderInfo.PairSymbols, orderInfo.Price,
                orderInfo.Amount, orderInfo.Exchange, orderInfo.Type);

            var id = await commandBus.SendAsync(command);
            var result = id.ToString();
            return result;
        }

        public async Task<OrderDetial> GetIdAsync(string id)
        {
            validator.VerifyId(id);

            var query = new GetOrderQuery { Id = id };
            var result = await queryBus.SendAsync(query);

            return mapper.Map(result);
        }

        public async Task<OrderList> GetList(int pageIndex, int pageSize)
        {
            PagingOptions paging = null;
            if (pageIndex > 0 && pageSize > 0)
            {
                paging = new PagingOptions { PageIndex = pageIndex, PageSize = pageSize };
            }
            var query = new GetOrdersQuery { PagingOptions = paging };

            var result = await queryBus.SendAsync(query);

            return mapper.Map(result);
        }
    }
}
