namespace SimulatedExchange.ClientAdapter.Messages
{
    public class FullCanceledMessage : IMessage
    {
        public OrderState State { get; set; }
    }
}
