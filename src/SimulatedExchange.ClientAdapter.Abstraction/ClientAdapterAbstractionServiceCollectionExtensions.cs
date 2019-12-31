using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.ClientAdapter.Abstraction.Bus;
using SimulatedExchange.ClientAdapter.Factories;

namespace SimulatedExchange.ClientAdapter
{
    public static class ClientAdapterAbstractionServiceCollectionExtensions
    {
        public static IServiceCollection AddClientAdapterCore(this IServiceCollection services)
        {
            services.AddBus();
            services.AddFactory();
            return services;
        }

        private static IServiceCollection AddBus(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBus, MessageBus>();

            return services;
        }

        private static IServiceCollection AddFactory(this IServiceCollection services)
        {
            services.AddSingleton<IMessageHanderFactory, MessageHandlerFactory>();

            return services;
        }

    }
}
