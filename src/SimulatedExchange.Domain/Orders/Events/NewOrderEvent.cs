using SimulatedExchange.Events;

namespace SimulatedExchange.Domain.Orders
{
    public class NewOrderEvent : Event
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
        // 订单类型
        public OrderType Type { get; set; }
    }
}
