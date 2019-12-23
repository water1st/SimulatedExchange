using SimulatedExchange.Messages;

namespace SimulatedExchange.Domain.Orders
{
    public class FullCanceledMessage : IMessage
    {
        public OrderState State { get; set; }
    }
}
