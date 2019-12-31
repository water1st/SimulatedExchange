using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Applications.Mapper;
using SimulatedExchange.Applications.Services;
using SimulatedExchange.Applications.Validators;

namespace SimulatedExchange.Applications
{
    public static class ApplicationsServiceCollectionExtensions
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
        {

            AddApplicationServices(services);
            AddMapper(services);
            AddValidator(services);
            return services;
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IOrderServices, OrderServices>();
            services.AddTransient<ITestServices, TestServices>();
        }

        private static void AddMapper(IServiceCollection services)
        {
            services.AddTransient<IOrderServiceMapper, OrderServiceMapper>();
        }

        private static void AddValidator(IServiceCollection services)
        {
            services.AddTransient<IOrderValidator, OrderValidator>();
        }

    }
}
