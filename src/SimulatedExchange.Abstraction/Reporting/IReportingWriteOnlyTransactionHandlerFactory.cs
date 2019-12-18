namespace SimulatedExchange.Reporting
{
    public interface IReportingWriteOnlyTransactionHandlerFactory
    {
        IReportingWriteOnlyTransactionHandler<TWriterParameter> GetWriter<TWriterParameter>() where TWriterParameter : class;
    }
}
