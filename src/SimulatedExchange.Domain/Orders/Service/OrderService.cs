using SimulatedExchange.Domain.Orders.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Orders.Service
{
    public class OrderService : IOrderService
    {
        public Task CancelOrderAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderInfo>> GetOrderListAsync()
        {
            throw new NotImplementedException();
        }

        public Task PlaceOrderAsync(OrderInfo orderInfo)
        {
            throw new NotImplementedException();
        }

        public Task TransactionAsync(TransactionInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
