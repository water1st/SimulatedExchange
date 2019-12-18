using SimulatedExchange.Events;

namespace SimulatedExchange.Domain.Orders
{
    public class TransactionEvent : Event
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
