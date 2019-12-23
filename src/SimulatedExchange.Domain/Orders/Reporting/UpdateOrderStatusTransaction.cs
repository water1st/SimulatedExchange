using System;

namespace SimulatedExchange.Domain.Orders
{
    public class UpdateOrderStatusTransaction
    {
        public string Id { get; set; }
        public OrderStatus Status { get; set; }
        //成交时间
        public DateTime DateTime { get; set; }
    }
}
