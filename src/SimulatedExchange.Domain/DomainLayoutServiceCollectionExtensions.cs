﻿using Microsoft.Extensions.DependencyInjection;
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
            services.AddTransient<IEventHandler<NewOrderEvent>, OrderReportMessageService>();
            services.AddTransient<IEventHandler<CancelOrderEvent>, OrderReportMessageService>();
            services.AddTransient<IEventHandler<TransactionEvent>, OrderReportMessageService>();

            services.AddTransient<IEventHandler<NewOrderEvent>, OrderWriteReportingService>();
            services.AddTransient<IEventHandler<CancelOrderEvent>, OrderWriteReportingService>();
            services.AddTransient<IEventHandler<TransactionEvent>, OrderWriteReportingService>();
        }

        private static void AddDomainServices(IServiceCollection services)
        {
            services.AddTransient<IOrderService, OrderService>();
        }
    }
}
