using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Orders.Service
{
    public interface IOrderService
    {
        //下单
        Task PlaceOrderAsync(OrderInfo orderInfo);
        //取消订单
        Task CancelOrderAsync(Guid id);
        //成交
        Task TransactionAsync(Guid Id, TransactionInfo info);
    }
}
