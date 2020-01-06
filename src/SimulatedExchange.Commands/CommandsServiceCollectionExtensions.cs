using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Commands.Bus;

namespace SimulatedExchange.Commands
{
    public static class CommandsServiceCollectionExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddCommandBus();

            return services;
        }


        private static IServiceCollection AddCommandBus(this IServiceCollection services)
        {
            services.AddSingleton<ICommandBus, MemoryCommandBus>();
            services.AddMediatR(typeof(ICommandBus).Assembly);
            return services;
        }
    }
}
