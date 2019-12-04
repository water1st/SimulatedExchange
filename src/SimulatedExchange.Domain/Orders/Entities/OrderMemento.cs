namespace SimulatedExchange.Domain.Orders.Entities
{
    public class OrderMemento : BaseMemento
    {
        public PairSymbols PairSymbols { get; set; }

        public decimal Price { get; set; }

        public decimal Volume { get; set; }

        public decimal TotalAmount { get; set; }

        public Exchange Exchange { get; set; }

        public OrderType Type { get; set; }

        public OrderStatus Status { get; set; }
    }
}
