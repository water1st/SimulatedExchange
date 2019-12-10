using SimulatedExchange.Domain.Orders.Entities;
using SimulatedExchange.Events;

namespace SimulatedExchange.Domain.Orders.Events
{
    public class NewOrder : Event
    {
        public NewOrder(PairSymbols symbols, decimal price, decimal amount, Exchange exchange)
        {
            Symbols = symbols;
            Price = price;
            Amount = amount;
            Exchange = exchange;
        }

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
