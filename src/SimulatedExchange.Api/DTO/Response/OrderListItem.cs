namespace SimulatedExchange.Api.DTO
{
    public class OrderListItem
    {
        public string Id { get; set; }
        //币对
        public string PairSymbols { get; set; }
        //委托价格 
        public decimal Price { get; set; }
        //委托总量
        public decimal TotalAmount { get; set; }
        //成交量
        public decimal Volume { get; set; }
        //订单状态
        public int Status { get; set; }
    }
}
