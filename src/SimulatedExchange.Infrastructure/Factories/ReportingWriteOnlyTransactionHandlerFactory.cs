using Microsoft.Extensions.DependencyInjection;
using SimulatedExchange.Reporting;
using System;

namespace SimulatedExchange.Infrastructure.Factories
{
    public class ReportingWriteOnlyTransactionHandlerFactory : IReportingWriteOnlyTransactionHandlerFactory
    {
        private readonly IServiceProvider provider;

        public ReportingWriteOnlyTransactionHandlerFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IReportingWriteOnlyTransactionHandler<TWriterParameter> GetWriter<TWriterParameter>() where TWriterParameter : class
        {
            return provider.GetService<IReportingWriteOnlyTransactionHandler<TWriterParameter>>();
        }
    }
}
