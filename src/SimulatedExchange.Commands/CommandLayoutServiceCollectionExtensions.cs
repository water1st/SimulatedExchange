using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Commands.Commands;
using SimulatedExchange.Commands.Handlers;

namespace SimulatedExchange.Commands
{
    public static class CommandLayoutServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandLayout(this IServiceCollection services)
        {
            services.AddCommandHandlers();

            return services;
        }

        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<AddOrderCommand>, AddOrderCommandHandler>();
            services.AddTransient<ICommandHandler<CancelOrderCommand>, CancelOrderCommandHandler>();
            services.AddTransient<ICommandHandler<OrderTransactionCommand>, OrderTransactionCommandHandler>();

            return services;
        }
    }
}
