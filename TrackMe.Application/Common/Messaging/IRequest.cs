using Common.Results;

namespace Application.Common.Messaging
{
    public interface IRequest<in TResponse> where TResponse : IResult
    {
    }

    public interface IRequestHandler<in TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
        where TResponse : IResult
    {
        Task<TResponse> Handle123(TRequest request, CancellationToken cancellationToken);
    }
}
