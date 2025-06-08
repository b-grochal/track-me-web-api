using Application.Common.Messaging;
using Common.Results;

namespace WebApi.Messaging
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public Task<Result> Send<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
        {
            //var handler = _serviceProvider.GetService<ICommandHandler<TCommand>>();
            //if (handler == null)
            //{
            //    throw new InvalidOperationException($"No handler registered for command type {typeof(TCommand).Name}");
            //}
            //return handler.Handle(command, cancellationToken);

            throw new NotImplementedException("This method is not implemented yet. Please implement the handler retrieval logic for ICommand<TResponse> commands.");
        }

        public Task<Result<TResponse>> Send<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand<TResponse>
        {
            //var handler = _serviceProvider.GetService<ICommandHandler<TCommand, TResponse>>();

            //if (handler == null)
            //{
            //    throw new InvalidOperationException($"No handler registered for command type {typeof(TCommand).Name}");
            //}

            //return handler.Handle(command, cancellationToken);

            throw new NotImplementedException("This method is not implemented yet. Please implement the handler retrieval logic for ICommand<TResponse> commands.");
        }
    }
}
