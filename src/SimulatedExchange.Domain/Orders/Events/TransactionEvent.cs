namespace SimulatedExchange.Domain.Orders
{
    public class TransactionEvent : OrderEvent
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}
