using SimulatedExchange.Messages;

namespace SimulatedExchange.Domain.Orders
{
    public class FullTransactionMessage : IMessage
    {
        public OrderState State { get; set; }
    }
}
