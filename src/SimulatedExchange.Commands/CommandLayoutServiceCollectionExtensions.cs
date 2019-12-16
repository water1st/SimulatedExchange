using Microsoft.Extensions.DependencyInjection;

namespace SimulatedExchange.Commands
{
    public static class CommandLayoutServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandLayout(this IServiceCollection services)
        {

            services.AddSingleton<ICommandHandlerFactory, CommandHandlerFactory>();

            return services;
        }
    }
}
