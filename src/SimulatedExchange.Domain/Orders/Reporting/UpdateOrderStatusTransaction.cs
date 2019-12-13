namespace SimulatedExchange.Domain.Orders
{
    public class UpdateOrderStatusTransaction
    {
        public string Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
