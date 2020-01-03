using SimulatedExchange.Events;

namespace SimulatedExchange.Domain.Orders
{
    public class PartialTransactionEvent : Event
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}
