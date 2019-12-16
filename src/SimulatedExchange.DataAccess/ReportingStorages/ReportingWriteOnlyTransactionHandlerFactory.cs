using SimulatedExchange.Reporting;
using System;

namespace SimulatedExchange.DataAccess.ReportingStorages
{
    public class ReportingWriteOnlyTransactionHandlerFactory : IReportingWriteOnlyTransactionHandlerFactory
    {
        private readonly IServiceProvider provider;

        public ReportingWriteOnlyTransactionHandlerFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IReportingWriteOnlyTransactionHandler<TWriterParameter> GetWriter<TWriterParameter>(TWriterParameter parameter) where TWriterParameter : class
        {
            var type = typeof(IReportingWriteOnlyTransactionHandler<>).MakeGenericType(parameter.GetType());
            return (IReportingWriteOnlyTransactionHandler<TWriterParameter>)provider.GetService(type);
        }
    }
}
