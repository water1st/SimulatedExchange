using SimulatedExchange.ClientAdapter.Messages;

namespace SimulatedExchange.Api.Hubs
{
    public class TradeMessage
    {
        public string Method { get; set; }
        public OrderState Parameter { get; set; }
    }
}
