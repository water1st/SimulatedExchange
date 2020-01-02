using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.DataAccess.Mapper;
using SimulatedExchange.DataAccess.Memory.EventSourcing;
using SimulatedExchange.EventSourcing;

namespace SimulatedExchange.DataAccess.Memory
{
    public static class MemoryDataAccessServiceCollectionExtensions
    {
        public static IServiceCollection AddMemoryProvider(this IServiceCollection services)
        {
            services.AddEventSourcing();
            services.AddReporting();
            services.AddMapper();

            return services;
        }

        private static IServiceCollection AddReporting(this IServiceCollection services)
        {
            services.AddProvider(typeof(MemoryDataAccessServiceCollectionExtensions).Assembly);
            return services;
        }

        private static IServiceCollection AddEventSourcing(this IServiceCollection services)
        {
            services.AddTransient<IEventStorage, EventStorage>();
            services.AddTransient<IMementoStorage, MementoStorage>();

            return services;
        }

        private static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddTransient<IOrderMapper, OrderMapper>();

            return services;
        }
    }
}
