using SimulatedExchange.Events;

namespace SimulatedExchange.Domain.Orders
{
    public class FullTransactionEvent : Event
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}
