using Application.Authentication.Common;
using Application.Common.Authentication;
using Application.Common.Data;
using Application.Common.Messaging;
using Common.Results;
using Domain.ApplicationUsers;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Login
{
    internal sealed class LoginCommandHandler(
        IApplicationDbContext dbContext,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider) : ICommandHandler<LoginCommand, LoginResponse>
    {
        public async Task<Result<LoginResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await dbContext.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

            if (user is null)
            {
                return Result.Failure<LoginResponse>(AuthenticationErrors.ApplicationUserNotFoundByEmail(command.Email));
            }

            bool isPasswordVerified = passwordHasher.Verify(user.PasswordHash, command.Password);

            if (!isPasswordVerified)
            {
                return Result.Failure<LoginResponse>(AuthenticationErrors.ApplicationUserNotFoundByEmail(command.Email));
            }

            string jwt = jwtProvider.Create(user);

            return new LoginResponse
            {
                Jwt = jwt
            };
        }
    }
}
