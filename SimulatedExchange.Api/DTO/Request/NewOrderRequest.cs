namespace SimulatedExchange.Api.DTO
{
    public class NewOrderRequest
    {
        public string PairSymbols { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}
