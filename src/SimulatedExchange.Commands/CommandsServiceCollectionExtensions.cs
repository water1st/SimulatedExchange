using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Commands.Bus;
using SimulatedExchange.Commands.Factory;
using SimulatedExchange.Commands.Handlers;

namespace SimulatedExchange.Commands
{
    public static class CommandsServiceCollectionExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddCommandHandlers();
            services.AddCommandBus();
            services.AddFactory();

            return services;
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<AddOrderCommand>, AddOrderCommandHandler>();
            services.AddTransient<ICommandHandler<CancelOrderCommand>, CancelOrderCommandHandler>();
            services.AddTransient<ICommandHandler<OrderTransactionCommand>, OrderTransactionCommandHandler>();

            return services;
        }

        private static IServiceCollection AddFactory(this IServiceCollection services)
        {
            services.AddSingleton<ICommandHandlerFactory, CommandHandlerFactory>();
            return services;
        }

        private static IServiceCollection AddCommandBus(this IServiceCollection services)
        {
            services.AddSingleton<ICommandBus, MemoryCommandBus>();
            return services;
        }
    }
}
