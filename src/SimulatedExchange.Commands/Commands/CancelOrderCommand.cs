using System;

namespace SimulatedExchange.Commands.Commands
{
    public class CancelOrderCommand : Command
    {
        public CancelOrderCommand(Guid id) : base(id)
        {

        }
    }
}
