using System;

namespace SimulatedExchange.Commands
{
    public class CancelOrderCommand : ICommand
    {
        public CancelOrderCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
