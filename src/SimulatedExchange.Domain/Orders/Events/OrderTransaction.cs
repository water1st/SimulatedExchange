using SimulatedExchange.Events;

namespace SimulatedExchange.Domain.Orders.Events
{
    public class OrderTransaction : Event
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}
