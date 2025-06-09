using Application.Common.Messaging;
using Common.Results;

namespace Application.Common.Authentication.Login
{
    public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, object>
    {
        Task<Result<object>> IRequestHandler<LoginCommand, Result<object>>.Handle123(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
