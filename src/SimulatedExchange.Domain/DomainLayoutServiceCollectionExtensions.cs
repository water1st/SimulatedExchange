using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Bus;
using SimulatedExchange.Domain.Bus;
using SimulatedExchange.Domain.Factory;
using SimulatedExchange.Domain.Orders;
using SimulatedExchange.Domain.Orders.Service;
using SimulatedExchange.Events;

namespace SimulatedExchange.Domain
{
    public static class DomainLayoutServiceCollectionExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            AddEventHandlers(services);
            AddDomainServices(services);
            AddRepositories(services);
            AddFactories(services);
            AddBus(services);

            return services;
        }

        private static void AddEventHandlers(IServiceCollection services)
        {
            services.AddTransient<IEventHandler<NewOrderEvent>, OrderReportService>();
            services.AddTransient<IEventHandler<PartialCancelOrderEvent>, OrderReportService>();
            services.AddTransient<IEventHandler<FullCancelOrderEvent>, OrderReportService>();
            services.AddTransient<IEventHandler<PartialTransactionEvent>, OrderReportService>();
            services.AddTransient<IEventHandler<FullTransactionEvent>, OrderReportService>();

            services.AddTransient<IEventHandler<NewOrderEvent>, OrderReportingService>();
            services.AddTransient<IEventHandler<PartialCancelOrderEvent>, OrderReportingService>();
            services.AddTransient<IEventHandler<FullCancelOrderEvent>, OrderReportingService>();
            services.AddTransient<IEventHandler<PartialTransactionEvent>, OrderReportingService>();
            services.AddTransient<IEventHandler<FullTransactionEvent>, OrderReportingService>();
        }

        private static void AddDomainServices(IServiceCollection services)
        {
            services.AddTransient<IOrderService, OrderService>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Order>, Repository<Order>>();
        }

        private static void AddFactories(IServiceCollection services)
        {
            services.AddSingleton<IEventHandlerFactory, EventHandlerFactory>();
        }

        private static void AddBus(IServiceCollection services)
        {
            services.AddSingleton<IEventBus, MemoryEventBus>();
        }
    }
}
