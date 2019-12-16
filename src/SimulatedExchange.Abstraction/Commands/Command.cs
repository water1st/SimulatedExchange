using System;

namespace SimulatedExchange.Commands
{
    public abstract class Command : ICommand
    {
        public Guid Id { get; private set; }
        public Command(Guid id)
        {
            Id = id;
        }
    }
}
