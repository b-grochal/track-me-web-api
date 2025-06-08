using Application.Common.Messaging;
using Common.Results;

namespace WebApi.Messaging
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public Task<Result<TResponse>> Send<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResponse>
        {
            var handler = _serviceProvider.GetService<IQueryHandler<TQuery, TResponse>>();

            if (handler == null)
            {
                throw new InvalidOperationException($"No handler registered for query type {typeof(TQuery).Name}");
            }

            return handler.Handle(query, cancellationToken);
        }
    }
}
