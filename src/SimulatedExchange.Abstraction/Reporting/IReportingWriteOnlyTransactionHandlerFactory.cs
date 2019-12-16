namespace SimulatedExchange.Reporting
{
    public interface IReportingWriteOnlyTransactionHandlerFactory
    {
        IReportingWriteOnlyTransactionHandler<TWriterParameter> GetWriter<TWriterParameter>(TWriterParameter parameter) where TWriterParameter : class;
    }
}
