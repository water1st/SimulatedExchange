using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Api.Hubs;
using SimulatedExchange.Domain.Orders;
using SimulatedExchange.Messages;

namespace SimulatedExchange.Api
{
    public static class ApiServiceCollectionExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            AddMessageHandler(services);
            return services;
        }

        public static void AddMessageHandler(IServiceCollection services)
        {
            services.AddTransient<IMessageHandler<OrderReportMessage>>(provider => provider.GetService<TradeReportHub>());
        }
    }
}
