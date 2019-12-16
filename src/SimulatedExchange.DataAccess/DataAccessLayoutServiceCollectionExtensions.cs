using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.DataAccess.Databases;
using SimulatedExchange.DataAccess.ReportingStorages.Orders;
using SimulatedExchange.DataAccess.Repositories;
using SimulatedExchange.DataAccess.Storages;
using SimulatedExchange.Domain;
using SimulatedExchange.Domain.Orders;
using SimulatedExchange.Queries.Orders;
using SimulatedExchange.Reporting;
using SimulatedExchange.Storages;

namespace SimulatedExchange.DataAccess
{
    public static class DataAccessLayoutServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessLayout(this IServiceCollection services)
        {
            AddDatabaseConnectionFactory(services);
            AddReportingStorages(services);
            AddRepositories(services);
            AddStorages(services);

            return services;
        }

        private static void AddDatabaseConnectionFactory(IServiceCollection services)
        {
            services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
        }

        private static void AddReportingStorages(IServiceCollection services)
        {
            services.AddTransient<IOrderReportingReadOnlyTransactionHandler, OrderReportingStorage>();
            services.AddTransient<IOrderReportingWriteOnlyTransactionHandler, OrderReportingStorage>();
            services.AddTransient<IReportingReadOnlyTransactionHandler<GetOrderTransaction, IOrderDetial>, OrderReportingStorage>();
            services.AddTransient<IReportingReadOnlyTransactionHandler<GetOrdersTransaction, IOrderList>, OrderReportingStorage>();
            services.AddTransient<IReportingWriteOnlyTransactionHandler<AddOrderTransaction>, OrderReportingStorage>();
            services.AddTransient<IReportingWriteOnlyTransactionHandler<UpdateOrderStatusTransaction>, OrderReportingStorage>();
            services.AddTransient<IReportingWriteOnlyTransactionHandler<UpdateOrderTransaction>, OrderReportingStorage>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Order>, Repository<Order>>();
        }

        private static void AddStorages(IServiceCollection services)
        {
            services.AddTransient<IEventStorage, MySQLEventStorage>();
            services.AddTransient<IMementoStorage, MySQLMementoStorage>();
        }
    }
}
