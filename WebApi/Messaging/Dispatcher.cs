using Application.Common.Messaging;
using Common.Results;
using System.Collections.Concurrent;

namespace WebApi.Messaging
{
    public interface IDispatcher
    {
        //Task<Result<object>> Dispatch<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        //    where TRequest : IRequest<Result<object>>;

        Task<TResponse> Dispatch<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IRequest<TResponse>
            where TResponse : Common.Results.IResult;

        //Task<TResponse> Dispatch<TResponse>(IRequest<TResponse> requset, CancellationToken cancellationToken = default)
        //    where TResponse : Common.Results.IResult;

        Task<TResponse> Dispatch<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        where TResponse : Common.Results.IResult;
    }

    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private static readonly ConcurrentDictionary<(Type, Type), Type> WrapperTypeCache = new();
        private static readonly ConcurrentDictionary<(Type, Type), Type> HandlerTypeCache = new();

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

        //public async Task<TResponse> Dispatch<TResponse>(IRequest<TResponse> requset, CancellationToken cancellationToken = default)
        //    where TResponse : Common.Results.IResult
        //{
        //    if (requset is null)
        //    {
        //        throw new ArgumentNullException(nameof(requset), "Request cannot be null");
        //    }

        //    var requestType = requset.GetType();
        //    var responseType = typeof(TResponse);

        //    var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, responseType);
        //    var handler = _serviceProvider.GetService(handlerType);

        //    if (handler == null)
        //    {
        //        throw new InvalidOperationException($"No handler registered for request type {requestType.Name}");
        //    }

        //    var method = handlerType.GetMethod("Handle123");

        //    if (method == null)
        //    {
        //        throw new InvalidOperationException($"Handler for request type {requestType.Name} does not implement Handle123 method");
        //    }

        //    // Fix for CS8600, CS8602, and CS8603: Ensure method.Invoke is not null and handle nullability properly
        //    var task = method.Invoke(handler, new object[] { requset, cancellationToken }) as Task<Common.Results.IResult>;
        //    if (task == null)
        //    {
        //        throw new InvalidOperationException($"Handler method invocation for request type {requestType.Name} did not return a valid Task<IResult>");
        //    }

        //    await task.ConfigureAwait(false);

        //    var resultProperty = task.GetType().GetProperty("Result");
        //    if (resultProperty == null)
        //    {
        //        throw new InvalidOperationException($"Task result property for request type {requestType.Name} is null");
        //    }

        //    var result = resultProperty.GetValue(task);
        //    if (result == null)
        //    {
        //        throw new InvalidOperationException($"Handler method result for request type {requestType.Name} is null");
        //    }

        //    return (TResponse)result;
        //}

        public async Task<TResponse> Dispatch<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        where TResponse : Common.Results.IResult
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            Type requestType = request.GetType();
            Type responseType = typeof(TResponse);

            Type handlerInterface = HandlerTypeCache.GetOrAdd(
                (requestType, responseType),
                key => typeof(IRequestHandler<,>).MakeGenericType(key.Item1, key.Item2));

            object? handler = _serviceProvider.GetService(handlerInterface);
            if (handler is null)
                throw new InvalidOperationException($"No handler registered for {handlerInterface.Name}");

            var wrapper = HandlerWrapper.Create(handler, requestType, responseType);
            var result = await wrapper.Handle(request, cancellationToken);

            return (TResponse)result;
        }

        // Base class for strongly-typed dispatch wrappers
        private abstract class HandlerWrapper
        {
            public abstract Task<object> Handle(object request, CancellationToken cancellationToken);

            public static HandlerWrapper Create(object handler, Type requestType, Type responseType)
            {
                var key = (requestType, responseType);

                var wrapperType = WrapperTypeCache.GetOrAdd(
                    key,
                    k => typeof(HandlerWrapper<,>).MakeGenericType(k.Item1, k.Item2));

                return (HandlerWrapper)Activator.CreateInstance(wrapperType, handler)!;
            }
        }

        // Generic wrapper: strongly typed call to IRequestHandler<TRequest, TResponse>.Handle123
        private sealed class HandlerWrapper<TRequest, TResponse>(object handler)
            : HandlerWrapper
            where TRequest : IRequest<TResponse>
            where TResponse : Common.Results.IResult
        {
            private readonly IRequestHandler<TRequest, TResponse> _handler = (IRequestHandler<TRequest, TResponse>)handler;

            public override async Task<object> Handle(object request, CancellationToken cancellationToken)
            {
                var typedRequest = (TRequest)request;
                var result = await _handler.Handle123(typedRequest, cancellationToken);
                return result!;
            }
        }
    }
}
