﻿using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Api.Hubs;
using SimulatedExchange.Api.Mapper;
using SimulatedExchange.ClientAdapter.Abstraction.Handlers;
using SimulatedExchange.ClientAdapter.Messages;


namespace SimulatedExchange.Api
{
    public static class ApiServiceCollectionExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            AddMessageHandler(services);
            AddMapper(services);
            return services;
        }

        private static void AddMessageHandler(IServiceCollection services)
        {
            services.AddTransient<IMessageHandler<NewOrderMessage>, TradeReportHubProxy>();
            services.AddTransient<IMessageHandler<PartialTransactionMessage>, TradeReportHubProxy>();
            services.AddTransient<IMessageHandler<FullTransactionMessage>, TradeReportHubProxy>();
            services.AddTransient<IMessageHandler<PartialCanceledMessage>, TradeReportHubProxy>();
            services.AddTransient<IMessageHandler<FullCanceledMessage>, TradeReportHubProxy>();
        }

        private static void AddMapper(IServiceCollection services)
        {
            services.AddTransient<IOrderMapper, OrderMapper>();
        }
    }
}
