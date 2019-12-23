namespace SimulatedExchange.Domain.Orders
{
    public class OrderInfo
    {
        public string ClientId { get; set; }
        //币对
        public PairSymbols Symbols { get; set; }
        //委托价格
        public decimal Price { get; set; }
        //委托总量
        public decimal Amount { get; set; }
        //交易所
        public Exchange Exchange { get; set; }
    }
}
