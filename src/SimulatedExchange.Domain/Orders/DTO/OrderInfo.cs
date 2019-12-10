using SimulatedExchange.Domain.Orders.Entities;

namespace SimulatedExchange.Domain.Orders.DTO
{
    public class OrderInfo
    {
        //币对
        public PairSymbols Symbols { get; private set; }
        //委托价格
        public decimal Price { get; private set; }
        //委托总量
        public decimal Amount { get; private set; }
        //交易所
        public Exchange Exchange { get; private set; }
    }
}
