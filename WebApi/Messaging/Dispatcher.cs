using Application.Common.Messaging;
using Common.Results;

namespace WebApi.Messaging
{
    public interface IDispatcher
    {
        Task<Result> Dispatch<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IRequest<Result>;
    }

    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public Dispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task<Result> Dispatch<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IRequest<Result>
        {
            var handler = _serviceProvider.GetService<IRequestHandler<TRequest, Result>>();

            if (handler == null)
            {
                throw new InvalidOperationException($"No handler registered for request type {typeof(TRequest).Name}");
            }

            return await handler.Handle123(request, cancellationToken);
        }
    }
}
