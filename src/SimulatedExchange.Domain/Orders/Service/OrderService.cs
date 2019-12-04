using SimulatedExchange.Domain.Orders.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimulatedExchange.Domain.Orders.Service
{
    public class OrderService : IOrderService
    {
        public Task CancelOrder(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderInfo>> GetOrderList()
        {
            throw new NotImplementedException();
        }

        public Task PlaceOrder(OrderInfo orderInfo)
        {
            throw new NotImplementedException();
        }

        public Task Transaction(TransactionInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
