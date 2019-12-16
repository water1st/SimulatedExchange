using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Domain.Orders;
using SimulatedExchange.Domain.Orders.Service;
using SimulatedExchange.Events;

namespace SimulatedExchange.Domain
{
    public static class DomainLayoutServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainLayout(this IServiceCollection services)
        {
            AddEventHandlers(services);
            AddDomainServices(services);

            return services;
        }

        private static void AddEventHandlers(IServiceCollection services)
        {
            services.AddSingleton<IEventHandlerFactory, EventHandlerFactory>();

            services.AddTransient<IEventHandler<NewOrderEvent>, OrderService>();
            services.AddTransient<IEventHandler<CancelOrderEvent>, OrderService>();
            services.AddTransient<IEventHandler<OrderTransactionEvent>, OrderService>();
        }

        private static void AddDomainServices(IServiceCollection services)
        {
            services.AddTransient<IOrderService, OrderService>();
        }
    }
}
