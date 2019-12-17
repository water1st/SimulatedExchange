using SimulatedExchange.Messages;

namespace SimulatedExchange.Domain.Orders
{
    public class OrderReportMessage : IMessage
    {
        public string Id { get; set; }
        public int Status { get; set; }
    }
}
