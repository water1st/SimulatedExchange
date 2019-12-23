using SimulatedExchange.Messages;

namespace SimulatedExchange.Domain.Orders
{
    public class NewOrderMessage : IMessage
    {
        public OrderState State { get; set; }
    }
}
