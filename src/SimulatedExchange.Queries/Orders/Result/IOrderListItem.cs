namespace SimulatedExchange.Queries.Orders
{
    public interface IOrderListItem
    {
        string Id { get; set; }
        //币对
        string PairSymbols { get; set; }
        //委托价格 
        decimal Price { get; set; }
        //委托总量
        decimal TotalAmount { get; set; }
        //交易所
        int Exchange { get; set; }
        //订单类型
        int Type { get; set; }
        //订单状态
        int Status { get; set; }
    }
}
