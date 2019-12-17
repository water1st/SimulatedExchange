using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Bus;
using SimulatedExchange.Commands;
using SimulatedExchange.Events;
using SimulatedExchange.Infrastructure.Bus;
using SimulatedExchange.Infrastructure.Factories;
using SimulatedExchange.Messages;
using SimulatedExchange.Reporting;

namespace SimulatedExchange.Infrastructure
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureLayout(this IServiceCollection services)
        {
            services.AddBus();
            services.AddFactories();
            return services;
        }

        private static IServiceCollection AddBus(this IServiceCollection services)
        {
            services.AddSingleton<ICommandBus, MemoryCommandBus>();
            services.AddSingleton<IEventBus, MemoryEventBus>();
            services.AddSingleton<IReadOnlyReportingBus, MemoryReadOnlyReportingBus>();
            services.AddSingleton<IWriteOnlyRepotingBus, MemoryWriteOnlyRepotingBus>();
            services.AddSingleton<IMessageBus, MemoryMessageBus>();

            return services;
        }

        private static IServiceCollection AddFactories(this IServiceCollection services)
        {
            services.AddSingleton<IEventHandlerFactory, EventHandlerFactory>();
            services.AddSingleton<ICommandHandlerFactory, CommandHandlerFactory>();
            services.AddSingleton<IReportingReadOnlyTransactionHandlerFactory, ReportingReadOnlyTransactionHandlerFactory>();
            services.AddSingleton<IReportingWriteOnlyTransactionHandlerFactory, ReportingWriteOnlyTransactionHandlerFactory>();
            services.AddSingleton<IMessageHandlerFactory, MessageHandlerFactory>();

            return services;
        }
    }
}
