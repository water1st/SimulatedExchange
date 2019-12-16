using Microsoft.AspNetCore.Mvc;
using SimulatedExchange.Api.DTO;
using SimulatedExchange.Api.Mapper;
using SimulatedExchange.Applications.DTO;
using SimulatedExchange.Applications.Services;
using System.Threading.Tasks;

namespace SimulatedExchange.Api.Controllers
{
    [Route("api/trades/order")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderServices orderService;
        private readonly IOrderMapper orderMapper;

        public OrdersController(IOrderServices orderService, IOrderMapper orderMapper)
        {
            this.orderService = orderService;
            this.orderMapper = orderMapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<GerOrderListResponse> GetList([FromQuery]GetOrderListRequest request)
        {
            var data = await orderService.GetList(request.PageIndex, request.PageSize);
            var result = orderMapper.Map(data);

            return result;
        }

        [HttpPost]
        [Route("")]
        public async Task NewOrder([FromBody]NewOrderRequest request)
        {
            await orderService.CreateNewOrder(new OrderInfo
            {
                Amount = request.Amount,
                PairSymbols = request.PairSymbols,
                Price = request.Price
            });
        }

        [HttpPut]
        [Route("{id}/deal")]
        public async Task Deal([FromBody]OrderDealRequest request)
        {
        }

        [HttpPut]
        [Route("{id}/cancel")]
        public async Task Cancel()
        {
        }
    }
}