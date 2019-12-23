using SimulatedExchange.Messages;

namespace SimulatedExchange.Domain.Orders
{
    public class CancelOrderMessage : IMessage
    {
        public OrderState State { get; set; }
    }
}
