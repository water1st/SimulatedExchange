using MediatR;

namespace SimulatedExchange.DataAccess.ReportingTransaction
{
    public interface IReportingTransaction : IRequest { }
    public interface IReportingTransaction<TResult> : IRequest<TResult> { }
}
