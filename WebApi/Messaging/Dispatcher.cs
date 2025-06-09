using Application.Common.Messaging;
using Common.Results;

namespace WebApi.Messaging
{
    public interface IDispatcher
    {
        //Task<Result<object>> Dispatch<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        //    where TRequest : IRequest<Result<object>>;

        Task<TResponse> Dispatch<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IRequest<TResponse>
            where TResponse : Common.Results.IResult;
    }

    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public Dispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        //public async Task<Result<object>> Dispatch<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        //    where TRequest : IRequest<Result<object>>
        //{
        //    var handler = _serviceProvider.GetService<IRequestHandler<TRequest, Result<object>>>();

        //    if (handler == null)
        //    {
        //        throw new InvalidOperationException($"No handler registered for request type {typeof(TRequest).Name}");
        //    }

        //    return await handler.Handle123(request, cancellationToken);
        //}

        public async Task<TResponse> Dispatch<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IRequest<TResponse>
            where TResponse : Common.Results.IResult
        {
            var handler = _serviceProvider.GetService<IRequestHandler<TRequest, TResponse>>();

            if (handler == null)
            {
                throw new InvalidOperationException($"No handler registered for request type {typeof(TRequest).Name}");
            }

            return await handler.Handle123(request, cancellationToken);
        }
    }
}
