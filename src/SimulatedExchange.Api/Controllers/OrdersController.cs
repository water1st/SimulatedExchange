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
        /// <summary>
        /// 获取订单列表
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<GerOrderListResponse> GetList([FromQuery]GetOrderListRequest request)
        {
            var data = await orderService.GetList(request.PageIndex, request.PageSize);
            var result = orderMapper.Map(data);

            return result;
        }
        /// <summary>
        /// 下单
        /// </summary>
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
        /// <summary>
        /// 成交
        /// </summary>
        [HttpPut]
        [Route("{id}/deal")]
        public async Task Deal([FromRoute]string id, [FromBody]OrderDealRequest request)
        {
            await orderService.TransactionAsync(id, request.Amount, request.Price);
        }
        /// <summary>
        /// 撤单
        /// </summary>
        [HttpPut]
        [Route("{id}/cancel")]
        public async Task Cancel([FromRoute]string id)
        {
            await orderService.CalcelOrderAsync(id);
        }
    }
}