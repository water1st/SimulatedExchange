namespace SimulatedExchange.ClientAdapter.Messages
{
    public class NewOrderMessage : IMessage
    {
        public OrderState State { get; set; }
    }
}
