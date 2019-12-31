using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Queries.Bus;
using SimulatedExchange.Queries.Handlers.Orders;
using SimulatedExchange.Queries.Mapper;
using SimulatedExchange.Queries.Orders;

namespace SimulatedExchange.Queries
{
    public static class QueryServiceCollectionExtensions
    {
        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddBus();
            services.AddHandlers();
            services.AddMapper();

            return services;
        }

        private static IServiceCollection AddBus(this IServiceCollection services)
        {
            services.AddSingleton<IQueryBus, QueryBus>();

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddTransient<IQueryHandler<GetOrderQuery, GetOrderQueryResult>, GetOrderQueryHandler>();
            services.AddTransient<IQueryHandler<GetOrdersQuery, GetOrdersQueryResult>, GetOrdersQueryHandler>();

            return services;
        }

        private static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddTransient<IOrderMapper, OrderMapper>();

            return services;
        }
    }
}
