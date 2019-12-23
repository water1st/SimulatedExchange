using System;

namespace SimulatedExchange.Domain.Orders
{
    public class UpdateOrderTransaction
    {
        public string Id { get; set; }
        public decimal Volume { get; set; }
        public OrderStatus Status { get; set; }
        //成交时间
        public DateTime DateTime { get; set; }
    }
}
