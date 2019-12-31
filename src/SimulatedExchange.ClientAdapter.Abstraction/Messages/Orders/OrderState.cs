namespace SimulatedExchange.ClientAdapter.Messages
{
    public class OrderState
    {
        //订单编号
        public string Id { get; set; }
        //客户端Id
        public string ClientId { get; set; }
        //币对
        public string PairSymbols { get; set; }
        //委托价格 
        public decimal Price { get; set; }
        //成交量
        public decimal Volume { get; set; }
        //委托总量
        public decimal TotalAmount { get; set; }
        //订单类型
        public int Type { get; set; }
        //订单状态
        public int Status { get; set; }
    }
}
