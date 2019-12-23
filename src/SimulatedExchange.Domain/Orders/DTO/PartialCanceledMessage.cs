using SimulatedExchange.Messages;

namespace SimulatedExchange.Domain.Orders
{
    public class PartialCanceledMessage : IMessage
    {
        public OrderState State { get; set; }
    }
}
