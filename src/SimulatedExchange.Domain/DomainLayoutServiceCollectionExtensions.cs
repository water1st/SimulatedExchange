using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Events;

namespace SimulatedExchange.Domain
{
    public static class DomainLayoutServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainLayout(IServiceCollection services)
        {
            AddEventHandlers(services);
            return services;
        }

        private static void AddEventHandlers(IServiceCollection services)
        {
            services.AddSingleton<IEventHandlerFactory, EventHandlerFactory>();


        }
    }
}
