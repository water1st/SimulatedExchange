using System;

namespace SimulatedExchange.Commands.Commands
{
    public class AddOrderCommand : Command
    {
        public AddOrderCommand(Guid id, string symbols, decimal price, decimal amount, int exchange, int type) : base(id)
        {
            Symbols = symbols ?? throw new ArgumentNullException(nameof(symbols));
            Price = price;
            Amount = amount;
            Exchange = exchange;
            Type = type;
        }

        //币对
        public string Symbols { get; }
        //委托价格
        public decimal Price { get; }
        //委托总量
        public decimal Amount { get; }
        //交易所
        public int Exchange { get; }
        // 订单类型
        public int Type { get; }
    }
}
