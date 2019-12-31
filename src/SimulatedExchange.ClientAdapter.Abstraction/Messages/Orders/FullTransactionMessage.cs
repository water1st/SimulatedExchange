namespace SimulatedExchange.ClientAdapter.Messages
{
    public class FullTransactionMessage : IMessage
    {
        public OrderState State { get; set; }
    }
}
