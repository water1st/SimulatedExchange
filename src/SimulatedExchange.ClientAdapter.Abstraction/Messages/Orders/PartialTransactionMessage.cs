namespace SimulatedExchange.ClientAdapter.Messages
{
    public class PartialTransactionMessage : IMessage
    {
        public OrderState State { get; set; }
    }
}
