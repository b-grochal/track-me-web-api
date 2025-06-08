using Application.Common.Messaging;
using Common.Results;

namespace WebApi.Messaging
{
    public interface ICommandDispatcher
    {
        Task<Result> Send<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand;
        Task<Result<TResponse>> Send<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand<TResponse>;
    }
}
