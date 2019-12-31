using SimulatedExchange.Commands;
using SimulatedExchange.Commands.Bus;
using System;
using System.Threading.Tasks;

namespace SimulatedExchange.Applications.Services
{
    public class TestServices : ITestServices
    {
        private readonly ICommandBus commandBus;

        public TestServices(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        public async Task TransactionAsync(string id, decimal amount, decimal price)
        {
            var command = new OrderTransactionCommand(Guid.Parse(id), price, amount);

            await commandBus.SendAsync(command);

        }
    }
}
