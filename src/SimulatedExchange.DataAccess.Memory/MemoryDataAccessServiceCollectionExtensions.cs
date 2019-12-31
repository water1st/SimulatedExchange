using Microsoft.Extensions.DependencyInjection;

namespace SimulatedExchange.DataAccess.Memory
{
    public static class MemoryDataAccessServiceCollectionExtensions
    {
        public static IServiceCollection AddMemoryProvider(this IServiceCollection services)
        {
            return services;
        }
    }
}
