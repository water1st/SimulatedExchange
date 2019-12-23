using SimulatedExchange.Messages;

namespace SimulatedExchange.Domain.Orders
{
    public class PartialTransactionMessage : IMessage
    {
        public OrderState State { get; set; }
    }
}
