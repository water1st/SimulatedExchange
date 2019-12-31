namespace SimulatedExchange.ClientAdapter.Messages
{
    public class PartialCanceledMessage : IMessage
    {
        public OrderState State { get; set; }
    }
}
