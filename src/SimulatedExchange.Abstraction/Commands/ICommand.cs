using System;

namespace SimulatedExchange.Commands
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}
