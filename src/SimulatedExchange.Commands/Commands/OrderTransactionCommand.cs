using System;

namespace SimulatedExchange.Commands.Commands
{
    public class OrderTransactionCommand : Command
    {
        public OrderTransactionCommand(Guid id, decimal price, decimal amount) : base(id)
        {
            Price = price;
            Amount = amount;
        }

        public decimal Price { get; }
        public decimal Amount { get; }
    }
}
