using SimulatedExchange.Messages;

namespace SimulatedExchange.Domain.Orders
{
    public class DealOrderMessage : IMessage
    {
        public OrderState State { get; set; }
    }
}
