namespace SimulatedExchange.Applications.DTO
{
    public class OrderInfo
    {
        //币对
        public string Symbols { get; set; }
        //委托价格
        public decimal Price { get; set; }
        //委托总量
        public decimal Amount { get; set; }
        //交易所
        public int Exchange { get; set; }
        public int Type { get; set; }
    }
}
