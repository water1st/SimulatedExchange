using SimulatedExchange.Domain.Orders.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Orders.Service
{
    public interface IOrderService
    {
        //下单
        Task PlaceOrder(OrderInfo orderInfo);
        //取消订单
        Task CancelOrder(Guid id);
        //成交
        Task Transaction(TransactionInfo info);
        //获取订单列表
        Task<IEnumerable<OrderInfo>> GetOrderList();
    }
}
