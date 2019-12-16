using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Bus;
using SimulatedExchange.Infrastructure.Bus;

namespace SimulatedExchange.Infrastructure
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureLayout(this IServiceCollection services)
        {
            services.AddBus();

            return services;
        }

        private static IServiceCollection AddBus(this IServiceCollection services)
        {
            services.AddSingleton<ICommandBus, MemoryCommandBus>();
            services.AddSingleton<IEventBus, MemoryEventBus>();
            services.AddSingleton<IReadOnlyReportingBus, MemoryReadOnlyReportingBus>();
            services.AddSingleton<IWriteOnlyRepotingBus, MemoryWriteOnlyRepotingBus>();

            return services;
        }
    }
}
