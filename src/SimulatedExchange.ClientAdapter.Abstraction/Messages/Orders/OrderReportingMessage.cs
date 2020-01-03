namespace SimulatedExchange.ClientAdapter.Messages.Orders
{
    public class OrderReportingMessage : IMessage
    {
        public Events Event { get; set; }
        public OrderState State { get; set; }
    }
}
