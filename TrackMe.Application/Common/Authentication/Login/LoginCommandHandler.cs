using Application.Common.Messaging;
using Common.Results;

namespace Application.Common.Authentication.Login
{
    public sealed class LoginCommandHandler : ICommandHandler<LoginCommand>
    {
        public Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Result.Success());
        }
    }
}
