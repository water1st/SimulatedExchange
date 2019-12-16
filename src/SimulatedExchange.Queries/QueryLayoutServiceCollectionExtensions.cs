using Microsoft.Extensions.DependencyInjection;

namespace SimulatedExchange.Queries
{
    public static class QueryLayoutServiceCollectionExtensions
    {
        public static IServiceCollection AddQueryLayout(this IServiceCollection services)
        {
            AddQyeryBus(services);

            return services;
        }

        private static IServiceCollection AddQyeryBus(this IServiceCollection services)
        {
            services.AddSingleton<IQueryBus, QueryBus>();
            return services;
        }
    }
}
