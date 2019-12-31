using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SimulatedExchange.DataAccess
{
    public static class DataAccessAbstractionServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessCore(this IServiceCollection services)
        {
            services.AddBus();

            return services;
        }

        private static IServiceCollection AddBus(this IServiceCollection services)
        {
            services.AddSingleton<ITransactionBus, TransactionBus>();

            return services;
        }

        public static IServiceCollection AddProvider(this IServiceCollection services, Assembly assembly)
        {
            services.AddMediatR(assembly);
            return services;
        }
    }
}
