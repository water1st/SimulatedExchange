using Microsoft.Extensions.DependencyInjection;

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
    }
}
