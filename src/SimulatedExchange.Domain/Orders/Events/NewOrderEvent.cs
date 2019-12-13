using SimulatedExchange.Events;

namespace SimulatedExchange.Domain.Orders
{
    public class NewOrderEvent : Event
    {
        public NewOrderEvent(PairSymbols symbols, decimal price, decimal amount, Exchange exchange, OrderType type)
        {
            Symbols = symbols;
            Price = price;
            Amount = amount;
            Exchange = exchange;
            Type = type;
        }
        //币对
        public PairSymbols Symbols { get; }
        //委托价格
        public decimal Price { get; }
        //委托总量
        public decimal Amount { get; }
        //交易所
        public Exchange Exchange { get; }
        // 订单类型
        public OrderType Type { get; }
    }
}
