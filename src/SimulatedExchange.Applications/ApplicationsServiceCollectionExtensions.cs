using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Applications.Mapper;
using SimulatedExchange.Commands;
using SimulatedExchange.Queries;

namespace SimulatedExchange.Applications
{
    public static class ApplicationsServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayout(this IServiceCollection services)
        {

            AddApplicationServices(services);
            AddMapper(services);

            return services;
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IOrderServiceMapper, OrderServiceMapper>();
        }

        private static void AddMapper(IServiceCollection services)
        {

        }
    }
}
