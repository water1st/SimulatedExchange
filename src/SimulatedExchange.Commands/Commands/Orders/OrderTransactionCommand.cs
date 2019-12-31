using System;

namespace SimulatedExchange.Commands
{
    public class OrderTransactionCommand : ICommand
    {
        public OrderTransactionCommand(Guid id, decimal price, decimal amount)
        {
            Price = price;
            Amount = amount;
            Id = id;
        }
        public Guid Id { get; }
        public decimal Price { get; }
        public decimal Amount { get; }
    }
}
