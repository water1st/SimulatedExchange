using SimulatedExchange.Events;

namespace SimulatedExchange.Domain.Orders
{
    public abstract class OrderEvent : Event
    {
        public OrderStatus Status { get; set; }
    }
}
