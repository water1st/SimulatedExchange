using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.DataAccess.EventSourcing;
using SimulatedExchange.DataAccess.Mapper;
using SimulatedExchange.DataAccess.Options;
using SimulatedExchange.DataAccess.ReportingTransaction;
using SimulatedExchange.DataAccess.ReportingTransaction.Orders;
using SimulatedExchange.EventSourcing;
using System;

namespace SimulatedExchange.DataAccess
{
    public static class MySQLDataAccessServiceCollectionExtensions
    {
        public static IServiceCollection AddMySQLProvider(this IServiceCollection services, Action<MySQLOptions> action)
        {
            services.Configure(action);

            AddFactory(services);
            AddReporting(services);
            AddEventSourcing(services);
            AddMapper(services);
            return services;
        }

        private static void AddFactory(IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        }

        private static void AddReporting(IServiceCollection services)
        {
            services.AddTransient<IReportingTransactionHandler<AddOrderTransaction>, OrderReporting>();
            services.AddTransient<IReportingTransactionHandler<UpdateOrderStatusTransaction>, OrderReporting>();
            services.AddTransient<IReportingTransactionHandler<UpdateOrderTransaction>, OrderReporting>();
            services.AddTransient<IReportingTransactionHandler<GetOrdersTransaction, GetOrdersTransactionResult>, OrderReporting>();
            services.AddTransient<IReportingTransactionHandler<GetOrderTransaction, GetOrderTransactionResult>, OrderReporting>();
        }

        private static void AddEventSourcing(IServiceCollection services)
        {
            services.AddTransient<IEventStorage, EventStorage>();
            services.AddTransient<IMementoStorage, MementoStorage>();
        }

        private static void AddMapper(IServiceCollection services)
        {
            services.AddTransient<IOrderMapper, OrderMapper>();
        }
    }
}
