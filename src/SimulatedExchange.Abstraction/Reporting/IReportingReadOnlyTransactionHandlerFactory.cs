namespace SimulatedExchange.Reporting
{
    public interface IReportingReadOnlyTransactionHandlerFactory
    {
        IReportingReadOnlyTransactionHandler<TReadParameter, TReadResult> GetReader<TReadParameter, TReadResult>()
            where TReadParameter : class
            where TReadResult : class;
    }
}
