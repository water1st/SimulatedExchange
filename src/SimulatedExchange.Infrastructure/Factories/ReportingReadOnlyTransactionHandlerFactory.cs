using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Reporting;
using System;

namespace SimulatedExchange.Infrastructure.Factories
{
    public class ReportingReadOnlyTransactionHandlerFactory : IReportingReadOnlyTransactionHandlerFactory
    {
        private readonly IServiceProvider provider;

        public ReportingReadOnlyTransactionHandlerFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IReportingReadOnlyTransactionHandler<TReadParameter, TReadResult> GetReader<TReadParameter, TReadResult>()
            where TReadParameter : class
            where TReadResult : class
        {
            return provider.GetService<IReportingReadOnlyTransactionHandler<TReadParameter, TReadResult>>();
        }
    }
}
