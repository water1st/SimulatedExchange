using MediatR;

namespace SimulatedExchange.Commands
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<TResult> : IRequest<TResult> { }
}
