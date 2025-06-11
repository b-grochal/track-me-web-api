using Application.Common.Messaging;
using Common.Results;

namespace Application.Common.Authentication.Login
{
    public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
    {
        async Task<Result<string>> IRequestHandler<LoginCommand, Result<string>>.Handle123(LoginCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(1000, cancellationToken); // Simulate some async work
            return Result.Success("");
        }
    }
}
