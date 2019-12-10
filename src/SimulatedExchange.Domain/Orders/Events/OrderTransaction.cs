using SimulatedExchange.Events;

namespace SimulatedExchange.Domain.Orders.Events
{
    public class OrderTransaction : Event
    {
        public OrderTransaction(decimal price, decimal amount)
        {
            Price = price;
            Amount = amount;
        }

        public decimal Price { get; private set; }
        public decimal Amount { get; private set; }
    }
}
