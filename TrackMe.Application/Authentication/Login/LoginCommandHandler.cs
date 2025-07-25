using Application.Common.Data;
using Application.Common.Messaging;
using Common.Results;

namespace Application.Authentication.Login
{
    internal sealed class LoginCommandHandler(IApplicationDbContext dbContext)
        : ICommandHandler<LoginCommand, string>
    {
        public async Task<Result<string>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            await Task.Delay(1000, cancellationToken); // Simulate some async work
            return Result.Success("");
        }
    }
}
