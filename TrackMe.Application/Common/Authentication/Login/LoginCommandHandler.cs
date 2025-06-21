using Application.Common.Messaging;
using Common.Results;

namespace Application.Common.Authentication.Login
{
    public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
    {
        public async Task<Result<string>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            await Task.Delay(1000, cancellationToken); // Simulate some async work
            return Result.Success("");
        }
    }
}
