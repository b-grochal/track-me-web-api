using Application.Common.Messaging;
using Common.Results;

namespace WebApi.Messaging
{
    public interface IQueryDispatcher
    {
        Task<Result<TResponse>> Send<TQuery, TResponse>(TQuery command, CancellationToken cancellationToken = default)
            where TQuery : IQuery<TResponse>;
    }
}
