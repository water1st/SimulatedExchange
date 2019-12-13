using SimulatedExchange.Events;

namespace SimulatedExchange.Domain.Orders
{
    public class OrderTransactionEvent : Event
    {
        public OrderTransactionEvent(decimal price, decimal amount)
        {
            Price = price;
            Amount = amount;
        }

        public decimal Price { get; }
        public decimal Amount { get; }
        public OrderStatus OrderStatus { get; set; }
    }
}
