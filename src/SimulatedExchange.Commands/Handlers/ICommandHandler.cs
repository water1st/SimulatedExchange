using MediatR;

namespace SimulatedExchange.Commands.Handlers
{
    internal interface ICommandHandler<TCommand> :
        IRequestHandler<TCommand> where TCommand : ICommand
    { }


    internal interface ICommandHandler<TCommand, TResult> :
        IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    { }

}
